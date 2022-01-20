using ContaObj.Domain.Model;

namespace ContaObj.Application.Interfaces;

public interface ITransacaoManager
{
    Task<Transacao> TranferirAsync(Transacao transacao);
}