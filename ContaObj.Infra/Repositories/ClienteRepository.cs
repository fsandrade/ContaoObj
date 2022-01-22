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
        return await context.Clientes
            .Include(x => x.Telefones)
            .Include(x => x.Endereco)
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<Cliente?> GetClienteAsync(int id)
    {
        return await context.Clientes.FindAsync(id);
    }

    public async Task<Cliente> InsertClienteAsync(Cliente cliente)
    {
        await context.Clientes.AddAsync(cliente);
        await context.SaveChangesAsync();
        return cliente;
    }

    public async Task<Cliente?> UpdateClienteAsync(Cliente cliente)
    {
        var clienteConsultado = await context.Clientes
            .Include(x => x.Telefones)
            .Include(x => x.Endereco)
            .Include(x => x.Contas)
            .FirstOrDefaultAsync(x => x.Id == cliente.Id);

        if (clienteConsultado == null)
        {
            return null;
        }

        context.Entry(clienteConsultado).CurrentValues.SetValues(cliente);
        context.Entry(clienteConsultado.Endereco).CurrentValues.SetValues(cliente.Endereco);
        clienteConsultado.Telefones = cliente.Telefones;

        await context.SaveChangesAsync();
        return cliente;
    }

    public async Task<Cliente?> InativarClienteAsync(int clienteId)
    {
        var clienteConsultado = await context.Clientes.
                                            Include(p => p.Contas)
                                            .FirstOrDefaultAsync(p => p.Id == clienteId);

        if (clienteConsultado == null)
        {
            return null;
        }

        clienteConsultado.Inativar();
        await context.SaveChangesAsync();
        return clienteConsultado;
    }

    public async Task<IEnumerable<Conta>> GetContasPorClienteAsync(int clienteId)
    {
        return await context.Contas
            .Where(x => x.Cliente.Id == clienteId)
            .ToListAsync();
    }

    public async Task<bool> ExistsOnDatabaseAsync(int id)
    {
        return await context.Clientes.FindAsync(id) != null;
    }
}