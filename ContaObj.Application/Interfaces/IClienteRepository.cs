
using ContaObj.Domain.Model;

namespace ContaObj.Application.Interfaces;

public interface IClienteRepository
{
    Task<Cliente> GetClienteAsync(int id);
    Task<IEnumerable<Cliente>> GetClientesAsync();
    Task<Cliente> InsertClienteAsync(Cliente cliente);
    Task<Cliente> UpdateClienteAsync(Cliente cliente);
}

