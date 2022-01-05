using ContaObj.Domain.Enumerations;
using ContaObj.Domain.Model;
using ContaObj.Infra.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ContaObj.Infra.Database;

public class ContaObjContext : DbContext
{
    public DbSet<Cliente> Clientes;
    public DbSet<Conta> Contas;
    public DbSet<Agencia> Agencias;
    public DbSet<Transacao> Transacoes;

    public ContaObjContext(DbContextOptions<ContaObjContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new ContaConfiguration());
        modelBuilder.ApplyConfiguration(new AgenciaConfiguration());
        modelBuilder.ApplyConfiguration(new EnderecoConfiguration());
        modelBuilder.ApplyConfiguration(new TransacaoConfiguration());
        modelBuilder.ApplyConfiguration(new BancoConfiguration());
        modelBuilder.ApplyConfiguration(new ClienteConfiguration());
        modelBuilder.ApplyConfiguration(new TelefoneConfiguration());
    }
}