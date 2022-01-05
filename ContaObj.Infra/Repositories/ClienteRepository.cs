using ContaObj.Application.Interfaces;
using ContaObj.Domain.Enumerations;
using ContaObj.Domain.Model;
using ContaObj.Infra.Database;
using Microsoft.EntityFrameworkCore;

namespace ContaObj.Infra.Repositories;

public class ClienteRepository : IClienteRepository
{
    private readonly ContaObjContext context;

    public ClienteRepository(ContaObjContext context)
    {
        this.context = context;
    }

    public async Task<IEnumerable<Cliente>> GetClientesAsync()
    {
        var clientes = await context.Clientes.AsNoTracking().ToListAsync();
        return clientes;
    }

    public async Task<Cliente> GetClienteAsync(int id)
    {
        return await context.Clientes.FindAsync(id);
    }

    public async Task<Cliente> InsertClienteAsync(Cliente cliente)
    {
        await context.Clientes.AddAsync(cliente);
        await context.SaveChangesAsync();
        return cliente;
    }

    public async Task<Cliente> UpdateClienteAsync(Cliente cliente)
    {
        var clienteConsultado = await context.Clientes.FindAsync(cliente.Id);

        if(clienteConsultado == null)
        {
            return null;
        }

        clienteConsultado.Endereco = cliente.Endereco;

        context.Entry(clienteConsultado).CurrentValues.SetValues(cliente);
        UpdateClienteTelefones(clienteConsultado, cliente);

        await context.SaveChangesAsync();
        return cliente;
    }

    public void UpdateClienteTelefones(Cliente clienteConsultado, Cliente cliente)
    {
        clienteConsultado.Telefones.Clear();
        foreach (var telefone in cliente.Telefones)
        {
            clienteConsultado.Telefones.Add(telefone);
        }
    }

    public async Task<bool?> InativarCliente(int clienteId)
    {
        var clienteConsultado = await context.Clientes.FindAsync(clienteId);

        if (clienteConsultado == null)
        {
            return null;
        }

        clienteConsultado.Status = StatusCliente.Inativo;
        await context.SaveChangesAsync();
        return true;
    }
}
