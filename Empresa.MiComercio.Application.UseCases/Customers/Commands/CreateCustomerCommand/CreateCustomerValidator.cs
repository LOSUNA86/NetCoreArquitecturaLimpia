using FluentValidation;

namespace Empresa.MiComercio.Application.UseCases.Customers.Commands.CreateCustomerCommand
{
    public class CreateCustomerValidator : AbstractValidator<CreateCustomerCommand>
    {
        public CreateCustomerValidator()
        {
            RuleFor(x => x.CustomerId).NotEmpty().NotNull().MinimumLength(5);
        }
    }
}
