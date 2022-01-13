using ContaObj.Domain.Model;

namespace ContaObj.Application.Interfaces;

public interface IClienteRepository
{
    Task<Cliente?> GetClienteAsync(int id);

    Task<IEnumerable<Cliente>> GetClientesAsync();

    Task<Cliente> InsertClienteAsync(Cliente cliente);

    Task<Cliente?> UpdateClienteAsync(Cliente cliente);

    Task<Cliente?> InativarClienteAsync(int clienteId);
    Task<IEnumerable<Conta>> GetContasPorClienteAsync(int clienteId);
    Task<bool> ExistsOnDatabaseAsync(int id);
}