using ContaObj.Domain.Model;

namespace ContaObj.Application.Interfaces
{
    public interface IContaRepository
    {
        Task<bool?> DepositarAsync(int contaId, decimal valorDeposito);
        Task<IEnumerable<Transacao>> ExtratoPorPeriodoAsync(int contaId, DateTime inicio, DateTime fim);
        Task<Conta> GetContaAsync(int contaId);
        Task<IEnumerable<Conta>> GetContasAsync();
        Task<IEnumerable<Conta>> GetContasPorClienteAsync(int clienteId);
        Task InativarContaAsync(int contaId);
        Task<Conta> InsertContaAsync(Conta novaConta);
        Task<bool?> SacarAsync(int contaId, decimal valorSaque);
        Task<bool?> TransferirDeAgenciaAsync(int contaId, int agenciaOrigemId, int agenciaDestinoId);
        Task<bool?> UpdateContaAsync(Conta alterarConta);
    }
}
