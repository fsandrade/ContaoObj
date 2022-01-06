
using AutoMapper;
using ContaObj.Application.Interfaces;
using ContaObj.Domain.ViewModel;
using ContaObj.Domain.Model;

namespace ContaObj.Application.Managers;

public class ClienteManager : IClienteManager
{
    private readonly IClienteRepository clienteRepository;
    private readonly IMapper mapper;

    public ClienteManager(IClienteRepository clienteRepository, IMapper mapper)
    {
        this.clienteRepository = clienteRepository;
        this.mapper = mapper;
    }

    public async Task<IEnumerable<ClienteViewModel>> GetClientesAsync()
    {
        var clientes = await clienteRepository.GetClientesAsync();
        return mapper.Map<IEnumerable<Cliente>, IEnumerable<ClienteViewModel>>(clientes);
    }

    public async Task<ClienteViewModel> GetClienteAsync(int id)
    {
        var cliente = await clienteRepository.GetClienteAsync(id);
        return mapper.Map<ClienteViewModel>(cliente);
    }

    public async Task<ClienteViewModel> InsertClienteAsync(NovoCliente novoCliente)
    {
        var cliente = mapper.Map<Cliente>(novoCliente);
        cliente = await clienteRepository.InsertClienteAsync(cliente);
        return mapper.Map<ClienteViewModel>(cliente);
    }

    public async Task<ClienteViewModel> UpdateClienteAsync(AlteraCliente alteraCliente)
    {
        var cliente = mapper.Map<Cliente>(alteraCliente);
        cliente = await clienteRepository.UpdateClienteAsync(cliente);
        return mapper.Map<ClienteViewModel>(cliente);
    }

    public async Task<bool?> InativarClienteAsync(int clienteId)
    {
        return await clienteRepository.InativarClienteAsync(clienteId);
    }
}

