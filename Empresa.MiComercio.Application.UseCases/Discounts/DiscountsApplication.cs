using AutoMapper;
using Empresa.MiComercio.Application.DTO;
using Empresa.MiComercio.Application.Interface.Infrastructure;
using Empresa.MiComercio.Application.Interface.Persistence;
using Empresa.MiComercio.Application.Interface.UseCases;
using Empresa.MiComercio.Domain.Entities;
using Empresa.MiComercio.Domain.Events;
using Empresa.MiComercio.Transversal.Common;

namespace Empresa.MiComercio.Application.UseCases.Discounts
{
    public class DiscountsApplication : IDiscountsApplication
    {
        private readonly IDiscountRepository _discountRepository;
        private readonly IMapper _mapper;
        //private readonly DiscountDtoValidator _discountDtoValidator;
        private readonly IEventBus _eventBus;
        private readonly INotification _notification;

        public DiscountsApplication(IDiscountRepository discountRepository, IMapper mapper, 
            //DiscountDtoValidator discountDtoValidator, 
            IEventBus eventBus, INotification notification)
        {
            _discountRepository = discountRepository;
            _mapper = mapper;
            //_discountDtoValidator = discountDtoValidator;
            _eventBus = eventBus;
            _notification = notification;
        }

        public async Task<Response<bool>> Create(DiscountDto discountDto, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();
            try
            {
                //var validation = await _discountDtoValidator.ValidateAsync(discountDto, cancellationToken);
                //if (!validation.IsValid)
                //{
                //    response.Message = "Errores de Validación";
                //    response.Errors = validation.Errors;
                //    return response;
                //}
                var discount = _mapper.Map<Discount>(discountDto);
                await _discountRepository.InsertAsync(discount);

                response.Data = await _discountRepository.Save(cancellationToken) > 0;
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Registro Exitoso!!!"; /* Publicamos el evento */

                    /* Publicamos el evento */
                    var discountCreatedEvent = _mapper.Map<DiscountCreatedEvent>(discount);
                    _eventBus.Publish(discountCreatedEvent);

                    /* Enviamos correo*/
                    //await _notification.SendMailAsync(response.Message, JsonSerializer.Serialize(discount), cancellationToken);

                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
                //await _notification.SendMailAsync(response.Message, JsonSerializer.Serialize(response), cancellationToken);
            }

            return response;
        }

        public async Task<Response<bool>> Delete(int id, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();
            try
            {
                await _discountRepository.DeleteAsync(id.ToString());
                response.Data = await _discountRepository.Save(cancellationToken) > 0;
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Eliminación Exitosa!!!";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<Response<DiscountDto>> Get(int id, CancellationToken cancellationToken = default)
        {
            var response = new Response<DiscountDto>();
            try
            {
                var discount = await _discountRepository.GetAsync(id, cancellationToken);
                if (discount is null)
                {
                    response.IsSuccess = true;
                    response.Message = "Descuento no existe...";
                    return response;
                }

                response.Data = _mapper.Map<DiscountDto>(discount);
                response.IsSuccess = true;
                response.Message = "Consulta Exitosa!!!";
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<Response<List<DiscountDto>>> GetAll(CancellationToken cancellationToken = default)
        {
            var response = new Response<List<DiscountDto>>();
            try
            {
                var discounts = await _discountRepository.GetAllAsync(cancellationToken);
                response.Data = _mapper.Map<List<DiscountDto>>(discounts);
                if (response.Data != null)
                {
                    response.IsSuccess = true;
                    response.Message = "Consulta Exitosa!!!";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }
            return response;
        }

        public async Task<ResponsePagination<IEnumerable<DiscountDto>>> GetAllWithPagination(int pageNumber, int pageSize)
        {
            var response = new ResponsePagination<IEnumerable<DiscountDto>>();
            try
            {
                var count = await _discountRepository.CountAsync();

                var customers = await _discountRepository.GetAllWithPaginationAsync(pageNumber, pageSize);
                response.Data = _mapper.Map<IEnumerable<DiscountDto>>(customers);

                if (response.Data != null)
                {
                    response.PageNumber = pageNumber;
                    response.TotalPages = (int)Math.Ceiling(count / (double)pageSize);
                    response.TotalCount = count;
                    response.IsSuccess = true;
                    response.Message = "Consulta Paginada Exitosa!!!";
                }
            }
            catch (Exception ex)
            {
                response.Message = ex.Message;
            }
            return response;
        }

        public async Task<Response<bool>> Update(DiscountDto discountDto, CancellationToken cancellationToken = default)
        {
            var response = new Response<bool>();
            try
            {
                //var validation = await _discountDtoValidator.ValidateAsync(discountDto, cancellationToken);
                //if (!validation.IsValid)
                //{
                //    response.Message = "Errores de Validación";
                //    response.Errors = validation.Errors;
                //    return response;
                //}
                var discount = _mapper.Map<Discount>(discountDto);
                await _discountRepository.UpdateAsync(discount);

                response.Data = await _discountRepository.Save(cancellationToken) > 0;
                if (response.Data)
                {
                    response.IsSuccess = true;
                    response.Message = "Actualización Exitosa!!!";
                }
            }
            catch (Exception e)
            {
                response.Message = e.Message;
            }

            return response;
        }
    }
}
