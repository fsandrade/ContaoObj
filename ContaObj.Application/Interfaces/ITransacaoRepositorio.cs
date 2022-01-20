using ContaObj.Domain.Model;
using ContaObj.Domain.ViewModel.Transacao;

namespace ContaObj.Application.Interfaces;

public interface ITransacaoRepositorio
{
    Task<RetornoTransacao> CriaTransacaoAsync(Transacao transacao);

    Task<Transacao> EfetivaDocAsync(Transacao transacao);
}