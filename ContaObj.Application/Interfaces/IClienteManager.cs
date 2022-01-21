using ContaObj.Domain.Model;

namespace ContaObj.Application.Interfaces;

public interface IClienteManager
{
    Task<Cliente?> GetClienteAsync(int id);

    Task<IEnumerable<Cliente>> GetClientesAsync();

    Task<Cliente?> InativarClienteAsync(int clienteId);

    Task<Cliente> InsertClienteAsync(Cliente novoCliente);

    Task<Cliente?> UpdateClienteAsync(Cliente alteraCliente);

    Task<IEnumerable<Conta>> GetContasPorClienteAsync(int clienteId);
}