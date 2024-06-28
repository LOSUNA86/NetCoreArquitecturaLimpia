using Dapper;
using Empresa.MiComercio.Domain.Entities;
using Empresa.MiComercio.Application.Interface.Persistence;
using Empresa.MiComercio.Persistence.Contexts;
using System.Data;

namespace Empresa.MiComercio.Persistence.Repositories
{
    public class CategoriesRepository : ICategoriesRepository
    {
        private readonly DapperContext _context;
        public CategoriesRepository(DapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Categories>> GetAll()
        {
            using var connection = _context.CreateConnection();
            var query = "Select * From Categories";

            var categories = await connection.QueryAsync<Categories>(query, commandType: CommandType.Text);
            return categories;
        }
    }
}
