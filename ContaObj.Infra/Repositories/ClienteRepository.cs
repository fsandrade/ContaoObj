using ContaObj.Application.Interfaces;
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
        return await context.Clientes.AsNoTracking().ToListAsync();
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

        context.Entry(clienteConsultado).CurrentValues.SetValues(cliente);

        await context.SaveChangesAsync();
        return cliente;
    }
}
