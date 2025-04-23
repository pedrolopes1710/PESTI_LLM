using dddnetcore.Domain.TiposEntregavel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dddnetcore.Infraestructure.TiposEntregavel
{
    public class TipoEntregavelEntityTypeConfiguration : IEntityTypeConfiguration<TipoEntregavel>
    {
        public void Configure(EntityTypeBuilder<TipoEntregavel> builder) 
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .HasConversion(
                    id => id.AsGuid(), 
                    guid => new TipoEntregavelId(guid)); 

            builder.Property(b => b.Nome)
                .HasConversion(
                    b => b.Nome, 
                    b => new NomeTipoEntregavel(b)) 
                .IsRequired();
        }
    }
}