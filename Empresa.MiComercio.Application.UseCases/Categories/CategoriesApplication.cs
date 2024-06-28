using AutoMapper;
using Empresa.MiComercio.Application.DTO.Response;
using Empresa.MiComercio.Application.Interface.UseCases;
using Empresa.MiComercio.Domain.Entities;
using Empresa.MiComercio.Application.Interface.Persistence;
using Empresa.MiComercio.Transversal.Common;
using Microsoft.Extensions.Caching.Distributed;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Empresa.MiComercio.Application.UseCases.Categories
{
    public class CategoriesApplication : ICategoriesApplication
    {
        private readonly ICategoriesRepository _categoriesRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CategoriesApplication> _logger;
        private readonly IDistributedCache _distributedCache;

        public CategoriesApplication(ICategoriesRepository categoriesRepository, IMapper mapper, ILogger<CategoriesApplication> logger, IDistributedCache distributedCache)
        {
            _categoriesRepository = categoriesRepository;
            _mapper = mapper;
            _logger = logger;
            _distributedCache = distributedCache;
        }

        public async Task<Response<IEnumerable<ResponseCategorie>>> GetAll()
        {
            var response = new Response<IEnumerable<ResponseCategorie>>();
            var cacheKey = "categoriesList";

            //try
            //{
                var redisCategories = await _distributedCache.GetAsync(cacheKey);
                if (redisCategories is not null)
                {
                    response.Data = JsonSerializer.Deserialize<IEnumerable<ResponseCategorie>>(redisCategories);
                }
                else
                {
                    var categories = await _categoriesRepository.GetAll();
                    response.Data = _mapper.Map<IEnumerable<ResponseCategorie>>(categories);

                    if (response.Data is not null)
                    {
                        var serializedCategories = Encoding.UTF8.GetBytes(JsonSerializer.Serialize(response.Data));
                        var options = new DistributedCacheEntryOptions()
                            .SetAbsoluteExpiration(DateTime.Now.AddHours(8))
                            .SetSlidingExpiration(TimeSpan.FromMinutes(60));

                        await _distributedCache.SetAsync(cacheKey, serializedCategories, options);
                    }
                }

            //}
            //catch (Exception e)
            //{
            //    response.Message = "Ocurrió un error al consultar las categorías. Contacte al administrador";
            //    response.HttpStatusCode = HttpStatusCode.InternalServerError;
            //    _logger.LogError(e.Message);
            //    return response;
            //}

            if (response.Data.Count() > 0)
            {
                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                response.Message = "Operación exitosa";
            }
            else
            {
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = "No se encontraron registros de categorías.";
            }

            return response;
        }
    }
}
