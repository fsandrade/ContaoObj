using ContaObj.Domain.Model;

namespace ContaObj.Application.Interfaces;

public interface ITransacaoManager
{
    Task<IEnumerable<Transacao>> ConsultaTransacoesAsync();
    Task<Transacao> TranferirAsync(Transacao transacao);
}