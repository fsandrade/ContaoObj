using ContaObj.Domain.Model;

namespace ContaObj.Application.Interfaces;

public interface ITransacaoRepositorio
{
    Task AtribuiContas(Transacao transacao);

    Task<Transacao> CriaTransacaoAsync(Transacao transacao);

    Task<Transacao> EfetivaDocAsync(Transacao transacao);

    Task<Transacao> ConsultaTransacaoExistenteAscyn(Guid id);
}