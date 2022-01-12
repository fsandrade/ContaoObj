using ContaObj.Domain.ViewModel;
using FluentValidation;

namespace ContaObj.Application.Validators
{
    public class NovoClienteValidator : AbstractValidator<NovoCliente>
    {
        public NovoClienteValidator()
        {
            RuleFor(x => x.Nome).NotNull().NotEmpty().MinimumLength(6).MaximumLength(100);
            RuleFor(x => x.Documento).NotNull().NotEmpty().MinimumLength(9).MaximumLength(14);
            RuleFor(x => x.DataNascimento).NotNull().NotEmpty().GreaterThan(DateTime.Now.AddYears(-120)).LessThan(DateTime.Now.AddYears(-18));
            RuleFor(x => x.Telefones).NotNull().NotEmpty();
            RuleFor(x => x.Sexo).NotEmpty().Must(BeMOrF);
            RuleFor(x => x.Endereco).SetValidator(new NovoEnderecoValidator());
            RuleForEach(x => x.Telefones).SetValidator(new NovoTelefoneValidator());
        }

        private bool BeMOrF(char arg)
        {
            return arg is 'M' or 'F';
        }
    }
}