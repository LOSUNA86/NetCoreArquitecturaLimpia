using AutoMapper;
using Empresa.MiComercio.Application.DTO;
using Empresa.MiComercio.Application.DTO.Response;
using Empresa.MiComercio.Application.UseCases.Customers.Commands.CreateCustomerCommand;
using Empresa.MiComercio.Application.UseCases.Customers.Commands.UpdateCustomerCommand;
using Empresa.MiComercio.Domain.Entities;
using Empresa.MiComercio.Domain.Events;

namespace Empresa.MiComercio.Application.UseCases.Common
{
    public class MappingsProfile : Profile
    {
        public MappingsProfile()
        {
            CreateMap<Empresa.MiComercio.Domain.Entities.Customers, CustomersDto>().ReverseMap();
            CreateMap<Empresa.MiComercio.Domain.Entities.Customers, ResponseCustomer>().ReverseMap();
            CreateMap<Empresa.MiComercio.Domain.Entities.Users, ResponseLogin>().ReverseMap();
            CreateMap<Empresa.MiComercio.Domain.Entities.Categories, ResponseCategorie>().ReverseMap();
            CreateMap<Discount, DiscountDto>().ReverseMap();
            CreateMap<Discount, DiscountCreatedEvent>().ReverseMap();

            CreateMap<Empresa.MiComercio.Domain.Entities.Customers, CreateCustomerCommand>().ReverseMap();
            CreateMap<Empresa.MiComercio.Domain.Entities.Customers, UpdateCustomerCommand>().ReverseMap();

            //CreateMap<Customers, CustomersDto>().ReverseMap()
            //    .ForMember(destination => destination.CustomerId, source => source.MapFrom(src => src.CustomerId))
            //    .ForMember(destination => destination.CompanyName, source => source.MapFrom(src => src.CompanyName))
            //    .ForMember(destination => destination.ContactName, source => source.MapFrom(src => src.ContactName))
            //    .ForMember(destination => destination.ContactTitle, source => source.MapFrom(src => src.ContactTitle))
            //    .ForMember(destination => destination.Address, source => source.MapFrom(src => src.Address))
            //    .ForMember(destination => destination.City, source => source.MapFrom(src => src.City))
            //    .ForMember(destination => destination.Region, source => source.MapFrom(src => src.Region))
            //    .ForMember(destination => destination.PostalCode, source => source.MapFrom(src => src.PostalCode))
            //    .ForMember(destination => destination.Country, source => source.MapFrom(src => src.Country))
            //    .ForMember(destination => destination.Phone, source => source.MapFrom(src => src.Phone))
            //    .ForMember(destination => destination.Fax, source => source.MapFrom(src => src.Fax)).ReverseMap();
        }
    }
}
