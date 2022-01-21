using ContaObj.Application.Interfaces;
using ContaObj.Domain.Model;

namespace ContaObj.Application.Managers;

public class ClienteManager : IClienteManager
{
    private readonly IClienteRepository clienteRepository;

    public ClienteManager(IClienteRepository clienteRepository)
    {
        this.clienteRepository = clienteRepository;
    }

    public async Task<IEnumerable<Cliente>> GetClientesAsync()
    {
        return await clienteRepository.GetClientesAsync();
    }

    public async Task<Cliente?> GetClienteAsync(int id)
    {
        return await clienteRepository.GetClienteAsync(id);
    }

    public async Task<Cliente> InsertClienteAsync(Cliente novoCliente)
    {
        return await clienteRepository.InsertClienteAsync(novoCliente);
    }

    public async Task<Cliente?> UpdateClienteAsync(Cliente alteraCliente)
    {
        return await clienteRepository.UpdateClienteAsync(alteraCliente);
    }

    public async Task<Cliente?> InativarClienteAsync(int clienteId)
    {
        return await clienteRepository.InativarClienteAsync(clienteId);
    }

    public async Task<IEnumerable<Conta>> GetContasPorClienteAsync(int clienteId)
    {
        return await clienteRepository.GetContasPorClienteAsync(clienteId);
    }
}