
using ContaObj.Domain.ViewModel;
using FluentValidation;

namespace ContaObj.Application.Validators
{
    public class NovoEnderecoValidator : AbstractValidator<NovoEndereco>
    {
        public NovoEnderecoValidator()
        {
            RuleFor(x => x.Cep).NotNull().NotEmpty();
        }
    }
}
