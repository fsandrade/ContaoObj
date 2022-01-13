using ContaObj.Domain.ViewModel;

namespace ContaObj.Application.Interfaces;

public interface IClienteManager
{
    Task<ClienteViewModel?> GetClienteAsync(int id);

    Task<IEnumerable<ClienteViewModel>> GetClientesAsync();

    Task<ClienteViewModel?> InativarClienteAsync(int clienteId);

    Task<ClienteViewModel> InsertClienteAsync(NovoCliente novoCliente);

    Task<ClienteViewModel?> UpdateClienteAsync(AlteraCliente alteraCliente);

    Task<IEnumerable<ContaViewModel>> GetContasPorClienteAsync(int clienteId);
}