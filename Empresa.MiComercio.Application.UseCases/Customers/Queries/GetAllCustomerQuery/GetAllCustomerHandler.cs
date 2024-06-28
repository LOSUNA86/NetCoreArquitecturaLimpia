using AutoMapper;
using Empresa.MiComercio.Application.DTO;
using Empresa.MiComercio.Application.Interface.Persistence;
using Empresa.MiComercio.Transversal.Common;
using MediatR;

namespace Empresa.MiComercio.Application.UseCases.Customers.Queries.GetAllCustomerQuery
{
    public class GetAllCustomerHandler : IRequestHandler<GetAllCustomerQuery, Response<IEnumerable<CustomersDto>>>
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly IMapper _mapper;

        public GetAllCustomerHandler(ICustomersRepository customersRepository, IMapper mapper)
        {
            _customersRepository = customersRepository;
            _mapper = mapper;
        }

        public async Task<Response<IEnumerable<CustomersDto>>> Handle(GetAllCustomerQuery request, CancellationToken cancellationToken)
        {
            var response = new Response<IEnumerable<CustomersDto>>();

            var customers = await _customersRepository.GetAllAsync();
            response.Data = _mapper.Map<IEnumerable<CustomersDto>>(customers);
            if (response.Data != null)
            {
                response.IsSuccess = true;
                response.Message = "Consulta Exitosa!!!";
            }

            return response;
        }
    }
}
