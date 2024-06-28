using Empresa.MiComercio.Domain.Entities;

namespace Empresa.MiComercio.Application.Interface.Persistence
{
    public interface IDiscountRepository : IGenericRepository<Discount>
    {
        Task<Discount> GetAsync(int id, CancellationToken cancellationToken);
        Task<List<Discount>> GetAllAsync(CancellationToken cancellationToken);
        Task<int> Save(CancellationToken cancellationToken);
    }
}
