using Empresa.MiComercio.Domain.Entities;

namespace Empresa.MiComercio.Application.Interface.Persistence
{
    public interface ICategoriesRepository
    {
        Task<IEnumerable<Categories>> GetAll();
    }
}
