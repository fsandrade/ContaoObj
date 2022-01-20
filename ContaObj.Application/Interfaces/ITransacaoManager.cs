using ContaObj.Domain.Model;
using ContaObj.Domain.ViewModel.Transacao;

namespace ContaObj.Application.Interfaces;

public interface ITransacaoManager
{
    Task<TransacaoViewModel> TranferirAsync(NovaTransacao transacao);
}