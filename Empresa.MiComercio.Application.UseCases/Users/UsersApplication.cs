using AutoMapper;
using Empresa.MiComercio.Application.DTO.Response;
using Empresa.MiComercio.Application.Interface.Persistence;
using Empresa.MiComercio.Application.Interface.UseCases;
using Empresa.MiComercio.Transversal.Common;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Empresa.MiComercio.Application.UseCases.Users
{
    public class UsersApplication : IUsersApplication
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;
        //private readonly UsersDtoValidator _usersDtoValidator;
        private readonly ILogger<UsersApplication> _logger;

        public UsersApplication(IUsersRepository usersRepository, IMapper iMapper, 
            //UsersDtoValidator usersDtoValidator, 
            ILogger<UsersApplication> logger)
        {
            _usersRepository = usersRepository;
            _mapper = iMapper;
            //_usersDtoValidator = usersDtoValidator;
            _logger = logger;
        }

        public async Task<Response<ResponseLogin>> Authenticate(string username, string password)
        {
            var response = new Response<ResponseLogin>();
            //var validation = _usersDtoValidator.Validate(new UsersDto() { UserName = username, Password = password });

            //if (!validation.IsValid)
            //{
            //    response.Message = "Errores de Validación";
            //    response.HttpStatusCode = HttpStatusCode.BadRequest;
            //    response.Errors = validation.Errors;
            //    return response;
            //}

            try
            {
                var user = await _usersRepository.Authenticate(username, password);

                if (user is not null)
                {
                    response.Data = _mapper.Map<ResponseLogin>(user);
                    response.HttpStatusCode = HttpStatusCode.OK;
                    response.IsSuccess = true;
                    response.Message = "Autenticación exitosa";
                }
                else
                {
                    response.IsSuccess = false;
                    response.HttpStatusCode = HttpStatusCode.BadRequest;
                    response.Message = "Credenciales incorrectas";
                }
            }
            catch (Exception e)
            {
                response.Message = "Ocurrió un error al relizar el login. Contacte al administrador";
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                _logger.LogError(e.Message);
            }

            return response;
        }
    }
}
