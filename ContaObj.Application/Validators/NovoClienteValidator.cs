
using ContaObj.Domain.ViewModel;
using FluentValidation;

namespace ContaObj.Application.Validators
{
    public class NovoClienteValidator : AbstractValidator<NovoCliente>
    {
        public NovoClienteValidator()
        {
            RuleFor(x => x.Nome).NotNull().NotEmpty().MinimumLength(6).MaximumLength(100);
        }
    }
}
