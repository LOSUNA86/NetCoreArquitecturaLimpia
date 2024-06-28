using Empresa.MiComercio.Application.DTO.Response;
using Empresa.MiComercio.Application.Interface.UseCases;
using Empresa.MiComercio.Transversal.Common;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using Asp.Versioning;

namespace Empresa.MiComercio.Services.WebApi.Controllers.v2
{
    [Authorize]
    [Route("api/v{version:apiVersion}/[controller]")]
    [ApiController]
    [ApiVersion("2.0")]
    [SwaggerTag("Get Categories of Products")]
    public class CategoriesController : ControllerBase
    {
        private readonly ICategoriesApplication _categoriesApplication;

        public CategoriesController(ICategoriesApplication categoriesApplication)
        {
            _categoriesApplication = categoriesApplication;
        }

        [HttpGet("GetAllAsync")]
        //[ProducesResponseType(typeof(Response<IEnumerable<ResponseCategorie>>), StatusCodes.Status200OK)]
        //[ProducesResponseType(StatusCodes.Status404NotFound)]
        //[ProducesResponseType(StatusCodes.Status500InternalServerError)]
        [SwaggerOperation(
        Summary = "Get Categories",
        Description = "This endpoint will return all categories",
        OperationId = "GetAll",
        Tags = new string[] { "GetAll" })]
        [SwaggerResponse(200, "List of Categories", typeof(Response<IEnumerable<ResponseCategorie>>))]
        [SwaggerResponse(404, "Notfound Categories")]
        public async Task<IActionResult> GetAllAsync()
        {
            var response = await _categoriesApplication.GetAll();
            return StatusCode((int)response.HttpStatusCode, response);
        }
    }
}
