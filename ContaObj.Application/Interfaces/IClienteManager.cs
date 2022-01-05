using ContaObj.Domain.ViewModel;

namespace ContaObj.Application.Interfaces;

public interface IClienteManager
{
    Task<ClienteViewModel> GetClienteAsync(int id);
    Task<IEnumerable<ClienteViewModel>> GetClientesAsync();
    Task<ClienteViewModel> InsertClienteAsync(NovoCliente novoCliente);
    Task<ClienteViewModel> UpdateClienteAsync(AlteraCliente alteraCliente);
}

