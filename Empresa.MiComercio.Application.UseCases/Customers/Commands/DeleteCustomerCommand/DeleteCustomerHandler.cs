using AutoMapper;
using Empresa.MiComercio.Application.Interface.Persistence;
using Empresa.MiComercio.Transversal.Common;
using MediatR;

namespace Empresa.MiComercio.Application.UseCases.Customers.Commands.DeleteCustomerCommand
{
    public class DeleteCustomerHandler : IRequestHandler<DeleteCustomerCommand, Response<bool>>
    {
        private readonly ICustomersRepository _customersRepository;
        private readonly IMapper _mapper;

        public DeleteCustomerHandler(ICustomersRepository customersRepository, IMapper mapper)
        {
            _customersRepository = customersRepository;
            _mapper = mapper;
        }

        public async Task<Response<bool>> Handle(DeleteCustomerCommand request, CancellationToken cancellationToken)
        {
            var response = new Response<bool>();

            response.Data = await _customersRepository.DeleteAsync(request.CustomerId);
            if (response.Data)
            {
                response.IsSuccess = true;
                response.Message = "Eliminación Exitosa!!!";
            }

            return response;
        }
    }
}
