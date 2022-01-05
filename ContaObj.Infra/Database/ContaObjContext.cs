using ContaObj.Domain.Model;
using ContaObj.Infra.Configuration;
using Microsoft.EntityFrameworkCore;

namespace ContaObj.Infra.Database;

public class ContaObjContext : DbContext
{
    public DbSet<Cliente> Clientes { get; set; }
    public DbSet<Conta> Contas { get; set; }
    public DbSet<Agencia> Agencias { get; set; }
    public DbSet<Transacao> Transacoes { get; set; }
    public DbSet<Telefone> Telefones { get; set; }
    public DbSet<Endereco> Enderecos { get; set; }


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