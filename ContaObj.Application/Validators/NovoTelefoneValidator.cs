
using ContaObj.Domain.ViewModel;
using FluentValidation;

namespace ContaObj.Application.Validators
{
    public class NovoTelefoneValidator : AbstractValidator<NovoTelefone>
    {
        public NovoTelefoneValidator()
        {
            RuleFor(p => p.Numero).NotEmpty().NotNull().MaximumLength(9);
            RuleFor(p => p.Ddd).NotEmpty().NotNull().GreaterThan(0);
        }
    }
}
