using Empresa.MiComercio.Application.DTO;
using Empresa.MiComercio.Application.DTO.Response;
using Empresa.MiComercio.Application.Interface.UseCases;
using Empresa.MiComercio.Services.WebApi.Helpers;
using Empresa.MiComercio.Transversal.Common;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using System.Text;
using Asp.Versioning;

namespace Empresa.MiComercio.Services.WebApi.Controllers.v2
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersApplication _usersApplication;        
        private readonly AppSettings _appSettings;

        public UsersController(IUsersApplication authApplication, IOptions<AppSettings> appSettings)
        {
            _usersApplication = authApplication;            
            _appSettings = appSettings.Value;
        }
        
        [AllowAnonymous]
        [HttpPost("Authenticate")]
        [ProducesResponseType(typeof(Response<ResponseLogin>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> Authenticate([FromBody] UsersDto usersDto)
        {
            var response = await _usersApplication.Authenticate(usersDto.UserName, usersDto.Password);

            if (response.IsSuccess)
                response.Data.Token = BuildToken(response);

            return StatusCode((int)response.HttpStatusCode, response);
        }
        
        [HttpPost("Renovar")]
        [ProducesResponseType(typeof(Response<string>), StatusCodes.Status200OK)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult Renovar()
        {
            Response<ResponseLogin> response = new Response<ResponseLogin>();
            response.Data = new ResponseLogin();            
            response.Data.UserName = HttpContext.User.Identity.Name;
            var token = BuildToken(response);

            Response<string> responseToken = new Response<string>();
            responseToken.Data = token;
            responseToken.IsSuccess = true;
            responseToken.Message = "Renovación de token exitosa";
            responseToken.HttpStatusCode = HttpStatusCode.OK;
            return StatusCode( (int) responseToken.HttpStatusCode, responseToken);
        }

        private string BuildToken(Response<ResponseLogin> responseLogin)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
            var claims = new Dictionary<string, object>
            {
                { "userid", responseLogin.Data.UserId.ToString() },
                { "username", responseLogin.Data.UserName }
            };

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, responseLogin.Data.UserName.ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(60),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
                Issuer = _appSettings.Issuer,
                Audience = _appSettings.Audience,
                Claims = claims
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            return tokenString;
        }
    }
}
