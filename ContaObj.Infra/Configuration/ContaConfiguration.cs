using ContaObj.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContaObj.Infra.Configuration;

public class ContaConfiguration : IEntityTypeConfiguration<Conta>
{
    public void Configure(EntityTypeBuilder<Conta> builder)
    {
        builder.HasOne(p => p.Cliente).WithMany().OnDelete(DeleteBehavior.NoAction);
    }
}