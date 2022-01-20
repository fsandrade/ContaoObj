using ContaObj.Domain.ViewModel.Transacao;
using FluentValidation;

namespace ContaObj.Application.Validators.Transacao;

public class NovaTransacaoValidator : AbstractValidator<NovaTransacao>
{
    public NovaTransacaoValidator()
    {
        RuleFor(p => p.Id).NotNull().NotEmpty();
        RuleFor(p => p.Valor).GreaterThan(0).NotNull().NotEmpty();
        //RuleFor(p => p.Origem).GreaterThan(999).NotNull().NotEmpty();
        //RuleFor(p => p.Destino).GreaterThan(999).NotNull().NotEmpty();
    }
}