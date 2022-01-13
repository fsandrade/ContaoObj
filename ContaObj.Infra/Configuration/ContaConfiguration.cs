using ContaObj.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContaObj.Infra.Configuration;

public class ContaConfiguration : IEntityTypeConfiguration<Conta>
{
    public void Configure(EntityTypeBuilder<Conta> builder)
    {
        builder.Property(p => p.Limite).HasColumnType("decimal(18,2)");
        builder.Property(p => p.Saldo).HasColumnType("decimal(18,2)");
        builder.HasOne(p => p.Cliente).WithMany(p => p.Contas).OnDelete(DeleteBehavior.NoAction);
    }
}