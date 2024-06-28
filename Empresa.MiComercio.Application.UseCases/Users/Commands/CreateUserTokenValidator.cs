using FluentValidation;

namespace Empresa.MiComercio.Application.UseCases.Users.Commands
{
    public class CreateUserTokenValidator : AbstractValidator<CreateUserTokenCommand>
    {
        public CreateUserTokenValidator()
        {
            RuleFor(u => u.userName).NotNull().NotEmpty();
            RuleFor(u => u.password).NotNull().NotEmpty().MinimumLength(5);
        }
    }
}
