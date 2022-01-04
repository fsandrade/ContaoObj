using ContaObj.Domain.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace ContaObj.Infra.Configuration
{
    public class AgenciaConfiguration : IEntityTypeConfiguration<Agencia>
    {
        public void Configure(EntityTypeBuilder<Agencia> builder)
        {
        }
    }
}