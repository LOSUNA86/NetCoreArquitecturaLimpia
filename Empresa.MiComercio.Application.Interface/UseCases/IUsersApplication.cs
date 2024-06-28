using Empresa.MiComercio.Application.DTO.Response;
using Empresa.MiComercio.Transversal.Common;

namespace Empresa.MiComercio.Application.Interface.UseCases
{
    public interface IUsersApplication
    {
        Task<Response<ResponseLogin>> Authenticate(string username, string password);
    }
}
