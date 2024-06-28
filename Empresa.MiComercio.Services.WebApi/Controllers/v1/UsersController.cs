using Empresa.MiComercio.Application.DTO;
using Empresa.MiComercio.Application.Interface.UseCases;
using Empresa.MiComercio.Services.WebApi.Helpers;
using Empresa.MiComercio.Transversal.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Asp.Versioning;

namespace Empresa.MiComercio.Services.WebApi.Controllers.v1
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("1.0", Deprecated = true)]
    public class UsersController : ControllerBase
    {
        private readonly IUsersApplication _usersApplication;
        private readonly AppSettings _appSettings;

        public UsersController(IUsersApplication authApplication, IOptions<AppSettings> appSettings)
        {
            _usersApplication = authApplication;
            _appSettings = appSettings.Value;
        }

        //[AllowAnonymous]
        //[HttpPost("Authenticate")]
        //public async Task<IActionResult> Authenticate([FromBody] UsersDto usersDto)
        //{
        //    var response = await _usersApplication.Authenticate(usersDto.UserName, usersDto.Password);
        //    if (response.IsSuccess)
        //    {
        //        if (response.Data != null)
        //        {
        //            response.Data.Token = BuildToken(response);
        //            return Ok(response);
        //        }
        //        else
        //            return NotFound(response);
        //    }

        //    return BadRequest(response);
        //}

        //private string BuildToken(Response<UsersDto> usersDto)
        //{
        //    var tokenHandler = new JwtSecurityTokenHandler();
        //    var key = Encoding.ASCII.GetBytes(_appSettings.Secret);
        //    var tokenDescriptor = new SecurityTokenDescriptor
        //    {
        //        Subject = new ClaimsIdentity(new Claim[]
        //        {
        //            new Claim(ClaimTypes.Name, usersDto.Data.UserId.ToString())
        //        }),
        //        Expires = DateTime.UtcNow.AddMinutes(30),
        //        SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature),
        //        Issuer = _appSettings.Issuer,
        //        Audience = _appSettings.Audience
        //    };
        //    var token = tokenHandler.CreateToken(tokenDescriptor);
        //    var tokenString = tokenHandler.WriteToken(token);
        //    return tokenString;
        //}
    }
}
