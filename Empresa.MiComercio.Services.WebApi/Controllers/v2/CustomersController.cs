using Empresa.MiComercio.Application.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Empresa.MiComercio.Transversal.Common;
using System.Net;
using Empresa.MiComercio.Application.DTO.Response;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.RateLimiting;
using Empresa.MiComercio.Application.Interface.UseCases;
using Asp.Versioning;

namespace Empresa.MiComercio.Services.WebApi.Controllers.v2
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersApplication _customersApplication;
        public CustomersController(ICustomersApplication customersApplication)
        {
            _customersApplication = customersApplication;
        }

        #region "Métodos Sincronos"

        [HttpPost("Insert")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Insert([FromBody] CustomersDto customersDto)
        {
            var response = _customersApplication.Insert(customersDto);
            return StatusCode((int)response.HttpStatusCode, response);            
        }

        [HttpPut("Update/{customerId}")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Update(string customerId, [FromBody] CustomersDto customersDto)
        {            
            var response = _customersApplication.Update(customerId, customersDto);
            return StatusCode( (int) response.HttpStatusCode, response);            
        }

        [HttpDelete("Delete/{customerId}")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Delete(string customerId)
        {            
            var response = _customersApplication.Delete(customerId);
            return StatusCode( (int) response.HttpStatusCode, response);
        }

        [HttpGet("Get/{customerId}")]
        [ProducesResponseType(typeof(Response<ResponseCustomer>), StatusCodes.Status200OK)]        
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult Get(string customerId)
        {            
            var response = _customersApplication.Get(customerId);
            return StatusCode( (int) response.HttpStatusCode, response);           
        }

        [HttpGet("GetAll")]
        [EnableRateLimiting("fixedWindow")]
        [ProducesResponseType(typeof(Response<IEnumerable<ResponseCustomer>>), StatusCodes.Status200OK)]        
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [ProducesResponseType(StatusCodes.Status429TooManyRequests)]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetAll()
        {
            var user = HttpContext.User.Identity.Name;
            Console.WriteLine(user);
            var response = _customersApplication.GetAll();
            return StatusCode( (int) response.HttpStatusCode, response);
        }

        [HttpGet("GetAllWithPagination")]
        [ProducesResponseType(typeof(Response<IEnumerable<ResponseCustomer>>), StatusCodes.Status200OK)]        
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public IActionResult GetAllWithPagination([FromQuery] int pageNumber, int pageSize)
        {
            var response = _customersApplication.GetAllWithPagination(pageNumber, pageSize);
            return StatusCode( (int) response.HttpStatusCode, response);
        }
        #endregion

        #region "Métodos Asincronos"

        [HttpPost("InsertAsync")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> InsertAsync([FromBody] CustomersDto customersDto)
        {
            var response = await _customersApplication.InsertAsync(customersDto);
            return StatusCode((int)response.HttpStatusCode, response);
        }

        [HttpPut("UpdateAsync/{customerId}")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> UpdateAsync(string customerId, [FromBody] CustomersDto customersDto)
        {
            var response = await _customersApplication.UpdateAsync(customerId, customersDto);
            return StatusCode((int)response.HttpStatusCode, response);
        }

        [HttpDelete("DeleteAsync/{customerId}")]
        [ProducesResponseType(typeof(Response<bool>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> DeleteAsync(string customerId)
        {
            var response = await _customersApplication.DeleteAsync(customerId);
            return StatusCode((int)response.HttpStatusCode, response);
        }

        [HttpGet("GetAsync/{customerId}")]
        [ProducesResponseType(typeof(Response<ResponseCustomer>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAsync(string customerId)
        {
            var response = await _customersApplication.GetAsync(customerId);
            return StatusCode((int)response.HttpStatusCode, response);
        }

        [HttpGet("GetAllAsync")]
        [ProducesResponseType(typeof(Response<IEnumerable<ResponseCustomer>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _customersApplication.GetAllAsync();
            return StatusCode((int)response.HttpStatusCode, response);
        }

        [HttpGet("GetAllWithPaginationAsync")]
        [ProducesResponseType(typeof(Response<IEnumerable<ResponseCustomer>>), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status500InternalServerError)]
        public async Task<IActionResult> GetAllWithPaginationAsync([FromQuery] int pageNumber, int pageSize)
        {
            var response = await _customersApplication.GetAllWithPaginationAsync(pageNumber, pageSize);
            return StatusCode((int)response.HttpStatusCode, response);
        }
        #endregion
    }
}
