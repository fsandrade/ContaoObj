using ContaObj.Domain.ViewModel;
using ContaObj.Domain.ViewModel.Conta;

namespace ContaObj.Application.Interfaces;

public interface IContaManager
{
    Task DepositarAsync(OperacaoPropriaConta deposito);

    Task<IEnumerable<TransacaoViewModel>> ExtratoPorPeriodoAsync(int contaId, DateTime inicio, DateTime fim);

    Task<ContaViewModel> GetContaAsync(int contaId);

    Task<IEnumerable<ContaViewModel>> GetContasAsync();

    Task InativarContaAsync(int contaId);

    Task<ContaViewModel?> InsertContaAsync(NovaConta novaConta);

    Task<bool?> SacarAsync(int contaId, decimal valorSaque);

    Task<bool?> UpdateContaAsync(AlteraConta alterarConta);
};