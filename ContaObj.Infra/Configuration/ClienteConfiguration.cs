using ContaObj.Domain.Enumerations;
using ContaObj.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContaObj.Infra.Configuration
{
    public class ClienteConfiguration : IEntityTypeConfiguration<Cliente>
    {
        public void Configure(EntityTypeBuilder<Cliente> builder)
        {
            builder.Property(p => p.Status).HasConversion(p => p.ToString(), p => (StatusCliente)Enum.Parse(typeof(StatusCliente), p));
        }
    }
}