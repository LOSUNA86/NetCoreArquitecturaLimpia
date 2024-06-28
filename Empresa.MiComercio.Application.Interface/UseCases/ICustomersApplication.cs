using Empresa.MiComercio.Application.DTO;
using Empresa.MiComercio.Application.DTO.Response;
using Empresa.MiComercio.Transversal.Common;

namespace Empresa.MiComercio.Application.Interface.UseCases
{
    public interface ICustomersApplication
    {
        #region Métodos Síncronos

        Response<bool> Insert(CustomersDto customersDto);
        Response<bool> Update(string customerId, CustomersDto customersDto);
        Response<bool> Delete(string customerId);

        Response<ResponseCustomer> Get(string customerId);
        Response<IEnumerable<ResponseCustomer>> GetAll();
        ResponsePagination<IEnumerable<ResponseCustomer>> GetAllWithPagination(int pageNumber, int pageSize);
        #endregion

        #region Métodos Asíncronos
        Task<Response<bool>> InsertAsync(CustomersDto customersDto);
        Task<Response<bool>> UpdateAsync(string customerId, CustomersDto customersDto);
        Task<Response<bool>> DeleteAsync(string customerId);

        Task<Response<ResponseCustomer>> GetAsync(string customerId);
        Task<Response<IEnumerable<ResponseCustomer>>> GetAllAsync();
        Task<ResponsePagination<IEnumerable<ResponseCustomer>>> GetAllWithPaginationAsync(int pageNumber, int pageSize);
        #endregion
    }
}
