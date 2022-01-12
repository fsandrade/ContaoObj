using ContaObj.Domain.ViewModel.Conta;
using FluentValidation;

namespace ContaObj.Application.Validators
{
    public class OperacaoPropriaContaValidator : AbstractValidator<OperacaoPropriaConta>
    {
        public OperacaoPropriaContaValidator()
        {
            RuleFor(x => x.ContaId).NotNull().GreaterThan(0);
            RuleFor(x => x.ValorTransacao).NotNull().GreaterThan(0);
        }

    }
}
