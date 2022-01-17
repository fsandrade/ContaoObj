using ContaObj.Domain.Model;
using ContaObj.Domain.ViewModel.Conta;

namespace ContaObj.Application.Interfaces
{
    public interface IContaRepository
    {
        Task DepositarAsync(OperacaoPropriaConta deposito);

        Task<IEnumerable<Transacao>> ExtratoPorPeriodoAsync(int contaId, DateTime inicio, DateTime fim);

        Task<Conta> GetContaAsync(int contaId);

        Task<IEnumerable<Conta>> GetContasAsync();

        Task InativarContaAsync(int contaId);

        Task<Conta> InsertContaAsync(Conta novaConta);

        Task SacarAsync(OperacaoPropriaConta saque);

        Task AlteraLimiteContaAsync(int contaId, decimal novoLimite);

        Task TransferirDeAgenciaAsync(Conta conta, int novaAgenciaId);
    }
}