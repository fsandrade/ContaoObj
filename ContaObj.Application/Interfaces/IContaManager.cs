
using ContaObj.Domain.ViewModel;

namespace ContaObj.Application.Interfaces;
public interface IContaManager
{
    Task<bool?> DepositarAsync(int contaId, decimal valorDeposito);
    Task<IEnumerable<TransacaoViewModel>> ExtratoPorPeriodoAsync(int contaId, DateTime inicio, DateTime fim);
    Task<ContaViewModel> GetContaAsync(int contaId);
    Task<IEnumerable<ContaViewModel>> GetContasAsync();
    Task<IEnumerable<ContaViewModel>> GetContasPorClienteAsync(int clienteId);
    Task InativarContaAsync(int contaId);
    Task<ContaViewModel> InsertContaAsync(NovaConta novaConta);
    Task<bool?> SacarAsync(int contaId, decimal valorSaque);
    Task<bool?> UpdateContaAsync(AlteraConta alterarConta);
};


