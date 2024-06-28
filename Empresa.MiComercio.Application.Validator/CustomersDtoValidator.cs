using Empresa.MiComercio.Application.DTO;
using FluentValidation;

namespace Empresa.MiComercio.Application.Validator
{
    public class CustomersDtoValidator : AbstractValidator<CustomersDto>
    {
        public CustomersDtoValidator()
        {            
            RuleFor(u => u.CompanyName).NotNull().NotEmpty().MaximumLength(40);
            RuleFor(u => u.ContactName).MaximumLength(40);
            RuleFor(u => u.ContactTitle).MaximumLength(30);
            RuleFor(u => u.Address).MaximumLength(60);
            RuleFor(u => u.City).MaximumLength(15);
            RuleFor(u => u.Region).MaximumLength(15);
            RuleFor(u => u.PostalCode).MaximumLength(10);
            RuleFor(u => u.Country).MaximumLength(15);
            RuleFor(u => u.Phone).MaximumLength(24);
            RuleFor(u => u.Fax).MaximumLength(24);
        }
    }
}
