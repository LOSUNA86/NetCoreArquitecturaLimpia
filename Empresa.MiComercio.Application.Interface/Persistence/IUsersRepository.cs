using Empresa.MiComercio.Domain.Entities;

namespace Empresa.MiComercio.Application.Interface.Persistence
{
    public interface IUsersRepository : IGenericRepository<Users>
    {
        Task<Users> Authenticate(string username, string password);

    }
}
