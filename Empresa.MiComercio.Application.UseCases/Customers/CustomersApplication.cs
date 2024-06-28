using AutoMapper;
using Empresa.MiComercio.Application.DTO;
using Empresa.MiComercio.Application.DTO.Response;
using Empresa.MiComercio.Application.Interface.Persistence;
using Empresa.MiComercio.Application.Interface.UseCases;
using Empresa.MiComercio.Transversal.Common;
using Microsoft.Extensions.Logging;
using System.Net;

namespace Empresa.MiComercio.Application.UseCases.Customers
{
    public class CustomersApplication : ICustomersApplication
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly IMapper _mapper;
        private readonly ILogger<CustomersApplication> _logger;
        //private readonly CustomersDtoValidator _customerValidation;

        public CustomersApplication(ICustomersRepository customersRepository, IMapper mapper, 
            ILogger<CustomersApplication> logger 
            //CustomersDtoValidator customerValidation
            )
        {
            _customersRepository = customersRepository;
            _mapper = mapper;
            _logger = logger;
            //_customerValidation = customerValidation;
        }

        #region Métodos Síncronos
        public Response<bool> Insert(CustomersDto customersDto)
        {
            var response = new Response<bool>();
            //var validation = _customerValidation.Validate(customersDto);

            //if (!validation.IsValid)
            //{
            //    response.Message = "Errores de Validación";
            //    response.HttpStatusCode = HttpStatusCode.BadRequest;
            //    response.Errors = validation.Errors;
            //    return response;
            //}

            var customer = _mapper.Map<Empresa.MiComercio.Domain.Entities.Customers>(customersDto);

            try
            {
                response.Data = _customersRepository.Insert(customer);
            }
            catch (Exception e)
            {
                response.Message = "Ocurrió un error al registrar el cliente. Contacte al administrador";
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                _logger.LogError(e.Message);
                return response;
            }

            if (response.Data)
            {
                response.HttpStatusCode = HttpStatusCode.Created;
                response.IsSuccess = true;
                response.Message = "El cliente se registró correctamente";
            }
            else
            {
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                response.Message = "No fue posible registrar al cliente";
            }

            return response;
        }

        public Response<bool> Update(string customerId, CustomersDto customersDto)
        {
            var response = new Response<bool>();

            //var validation = _customerValidation.Validate(customersDto);

            //if (!validation.IsValid)
            //{
            //    response.Message = "Errores de Validación";
            //    response.Errors = validation.Errors;
            //    response.HttpStatusCode = HttpStatusCode.BadRequest;
            //    return response;
            //}

            var customer = _mapper.Map<Empresa.MiComercio.Domain.Entities.Customers>(customersDto);
            customer.CustomerId = customerId;

            try
            {
                response.Data = _customersRepository.Update(customer);
            }
            catch (Exception e)
            {
                response.Message = "Ocurrió un error al actualizar el cliente. Contacte al administrador";
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                _logger.LogError(e.Message);
                return response;
            }

            if (response.Data)
            {
                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                response.Message = "El cliente se actualizó correctamente";
            }
            else
            {
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                response.Message = "No fue posible actualizar al cliente";
            }

            return response;
        }

        public Response<bool> Delete(string customerId)
        {
            var response = new Response<bool>();

            try
            {
                response.Data = _customersRepository.Delete(customerId);
            }
            catch (Exception e)
            {
                response.Message = "Ocurrió un error al eliminar el cliente. Contacte al administrador";
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                _logger.LogError(e.Message);
                return response;
            }

            if (response.Data)
            {
                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                response.Message = "El cliente se eliminó correctamente";
            }
            else
            {
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                response.Message = "No fue posible eliminar el cliente";
            }

            return response;
        }

        public Response<ResponseCustomer> Get(string customerId)
        {
            var response = new Response<ResponseCustomer>();

            try
            {
                var customer = _customersRepository.Get(customerId);
                response.Data = _mapper.Map<ResponseCustomer>(customer);
            }
            catch (Exception e)
            {
                response.Message = "Ocurrió un error al consultar el cliente. Contacte al administrador";
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                _logger.LogError(e.Message);
                return response;
            }

            if (response.Data is not null)
            {
                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                response.Message = "Operación exitosa";
            }
            else
            {
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"No se encontró el cliente con el id {customerId}";
            }

            return response;
        }

        public Response<IEnumerable<ResponseCustomer>> GetAll()
        {
            var response = new Response<IEnumerable<ResponseCustomer>>();

            try
            {
                var customers = _customersRepository.GetAll();
                response.Data = _mapper.Map<IEnumerable<ResponseCustomer>>(customers);
            }
            catch (Exception e)
            {
                response.Message = "Ocurrió un error al consultar los clientes. Contacte al administrador";
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                _logger.LogError(e.Message);
                return response;
            }

            if (response.Data.Count() > 0)
            {
                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                response.Message = "Operación exitosa";
            }
            else
            {
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = "No se encontraron registros de clientes.";
            }

            return response;
        }

        public ResponsePagination<IEnumerable<ResponseCustomer>> GetAllWithPagination(int pageNumber, int pageSize)
        {
            var response = new ResponsePagination<IEnumerable<ResponseCustomer>>();
            var count = 0;

            try
            {
                count = _customersRepository.Count();
                var customers = _customersRepository.GetAllWithPagination(pageNumber, pageSize);
                response.Data = _mapper.Map<IEnumerable<ResponseCustomer>>(customers);
            }
            catch (Exception e)
            {
                response.Message = "Ocurrió un error al consultar los clientes. Contacte al administrador";
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                _logger.LogError(e.Message);
                return response;
            }

            if (response.Data.Count<CustomersDto>() > 0)
            {
                response.PageNumber = pageNumber;
                response.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                response.TotalCount = count;
                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                response.Message = "Operación exitosa";
            }
            else
            {
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = "No se encontraron registros de clientes";
            }

            return response;
        }
        #endregion

        #region Métodos Asíncronos
        public async Task<Response<bool>> InsertAsync(CustomersDto customersDto)
        {
            var response = new Response<bool>();
            //var validation = _customerValidation.Validate(customersDto);

            //if (!validation.IsValid)
            //{
            //    response.Message = "Errores de Validación";
            //    response.HttpStatusCode = HttpStatusCode.BadRequest;
            //    response.Errors = validation.Errors;
            //    return response;
            //}

            var customer = _mapper.Map<Empresa.MiComercio.Domain.Entities.Customers>(customersDto);

            try
            {
                response.Data = await _customersRepository.InsertAsync(customer);
            }
            catch (Exception e)
            {
                response.Message = "Ocurrió un error al registrar el cliente. Contacte al administrador";
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                _logger.LogError(e.Message);
                return response;
            }

            if (response.Data)
            {
                response.HttpStatusCode = HttpStatusCode.Created;
                response.IsSuccess = true;
                response.Message = "El cliente se registró correctamente";
            }
            else
            {
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                response.Message = "No fue posible registrar al cliente";
            }

            return response;
        }

        public async Task<Response<bool>> UpdateAsync(string customerId, CustomersDto customersDto)
        {
            var response = new Response<bool>();

            //var validation = _customerValidation.Validate(customersDto);

            //if (!validation.IsValid)
            //{
            //    response.Message = "Errores de Validación";
            //    response.Errors = validation.Errors;
            //    response.HttpStatusCode = HttpStatusCode.BadRequest;
            //    return response;
            //}

            var customer = _mapper.Map<Empresa.MiComercio.Domain.Entities.Customers>(customersDto);
            customer.CustomerId = customerId;

            try
            {
                response.Data = await _customersRepository.UpdateAsync(customer);
            }
            catch (Exception e)
            {
                response.Message = "Ocurrió un error al actulizar el cliente. Contacte al administrador";
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                _logger.LogError(e.Message);
                return response;
            }

            if (response.Data)
            {
                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                response.Message = "El cliente se actualizó correctamente";
            }
            else
            {
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                response.Message = "No fue posible actualizar al cliente";
            }

            return response;
        }

        public async Task<Response<bool>> DeleteAsync(string customerId)
        {
            var response = new Response<bool>();

            try
            {
                response.Data = await _customersRepository.DeleteAsync(customerId);
            }
            catch (Exception e)
            {
                response.Message = "Ocurrió un error al eliminar el cliente. Contacte al administrador";
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                _logger.LogError(e.Message);
                return response;
            }

            if (response.Data)
            {
                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                response.Message = "El cliente se eliminó correctamente";
            }
            else
            {
                response.HttpStatusCode = HttpStatusCode.BadRequest;
                response.Message = "No fue posible eliminar el cliente";
            }

            return response;
        }

        public async Task<Response<ResponseCustomer>> GetAsync(string customerId)
        {
            var response = new Response<ResponseCustomer>();

            try
            {
                var customer = await _customersRepository.GetAsync(customerId);
                response.Data = _mapper.Map<ResponseCustomer>(customer);
            }
            catch (Exception e)
            {
                response.Message = "Ocurrió un error al consultar el cliente. Contacte al administrador";
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                _logger.LogError(e.Message);
                return response;
            }

            if (response.Data is not null)
            {
                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                response.Message = "Operación exitosa";
            }
            else
            {
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = $"No se encontró el cliente con el id {customerId}";
            }

            return response;
        }

        public async Task<Response<IEnumerable<ResponseCustomer>>> GetAllAsync()
        {
            var response = new Response<IEnumerable<ResponseCustomer>>();

            try
            {
                var customers = await _customersRepository.GetAllAsync();
                response.Data = _mapper.Map<IEnumerable<ResponseCustomer>>(customers);
            }
            catch (Exception e)
            {
                response.Message = "Ocurrió un error al consultar los clientes. Contacte al administrador";
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                _logger.LogError(e.Message);
                return response;
            }

            if (response.Data.Count() > 0)
            {
                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                response.Message = "Operación exitosa";
            }
            else
            {
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = "No se encontraron registros de clientes.";
            }

            return response;
        }

        public async Task<ResponsePagination<IEnumerable<ResponseCustomer>>> GetAllWithPaginationAsync(int pageNumber, int pageSize)
        {
            var response = new ResponsePagination<IEnumerable<ResponseCustomer>>();
            var count = 0;

            try
            {
                count = await _customersRepository.CountAsync();
                var customers = await _customersRepository.GetAllWithPaginationAsync(pageNumber, pageSize);
                response.Data = _mapper.Map<IEnumerable<ResponseCustomer>>(customers);
            }
            catch (Exception e)
            {
                response.Message = "Ocurrió un error al consultar los clientes. Contacte al administrador";
                response.HttpStatusCode = HttpStatusCode.InternalServerError;
                _logger.LogError(e.Message);
                return response;
            }

            if (response.Data.Count<CustomersDto>() > 0)
            {
                response.PageNumber = pageNumber;
                response.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                response.TotalCount = count;
                response.IsSuccess = true;
                response.HttpStatusCode = HttpStatusCode.OK;
                response.Message = "Operación exitosa";
            }
            else
            {
                response.HttpStatusCode = HttpStatusCode.NotFound;
                response.Message = "No se encontraron registros de clientes.";
            }

            return response;
        }
        #endregion
    }
}
