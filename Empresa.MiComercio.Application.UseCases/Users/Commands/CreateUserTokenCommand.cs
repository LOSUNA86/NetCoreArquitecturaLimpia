using Empresa.MiComercio.Application.DTO;
using Empresa.MiComercio.Application.DTO.Response;
using Empresa.MiComercio.Transversal.Common;
using MediatR;

namespace Empresa.MiComercio.Application.UseCases.Users.Commands
{
    public sealed record CreateUserTokenCommand : IRequest<Response<ResponseLogin>>
    {
        public string userName { get; set; }
        public string password { get; set; }
    }
}
