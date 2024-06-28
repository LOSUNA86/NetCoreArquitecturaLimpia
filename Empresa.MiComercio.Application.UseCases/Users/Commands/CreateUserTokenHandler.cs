using AutoMapper;
using Empresa.MiComercio.Application.DTO;
using Empresa.MiComercio.Application.DTO.Response;
using Empresa.MiComercio.Application.Interface.Persistence;
using Empresa.MiComercio.Transversal.Common;
using MediatR;

namespace Empresa.MiComercio.Application.UseCases.Users.Commands
{
    public class CreateUserTokenHandler : IRequestHandler<CreateUserTokenCommand, Response<ResponseLogin>>
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IMapper _mapper;

        public CreateUserTokenHandler(IUsersRepository usersRepository, IMapper mapper)
        {
            _usersRepository = usersRepository;
            _mapper = mapper;
        }

        public async Task<Response<ResponseLogin>> Handle(CreateUserTokenCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<ResponseLogin>();
            var user = await _usersRepository.Authenticate(request.userName, request.password);
            if (user is null)
            {
                response.IsSuccess = true;
                response.Message = "Usuario no existe";
                return response;
            }

            response.Data = _mapper.Map<ResponseLogin>(user);
            response.IsSuccess = true;
            response.Message = "Autenticación Exitosa!!!";

            return response;
        }
    }
}
