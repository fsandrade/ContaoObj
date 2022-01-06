using ContaObj.Domain.Enumerations;
using ContaObj.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContaObj.Infra.Configuration;

public class ContaConfiguration : IEntityTypeConfiguration<Conta>
{
    public void Configure(EntityTypeBuilder<Conta> builder)
    {
        builder.Property(p => p.Status).HasConversion(p => p.ToString(), p => (StatusConta)Enum.Parse(typeof(StatusConta), p));
        builder.Property(p => p.Numero).ValueGeneratedOnAdd();
        builder.HasOne(p => p.Cliente).WithMany().OnDelete(DeleteBehavior.NoAction);
    }
}