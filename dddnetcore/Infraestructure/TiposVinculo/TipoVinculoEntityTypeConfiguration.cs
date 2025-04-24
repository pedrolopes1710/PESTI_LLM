using dddnetcore.Domain.TiposVinculo;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dddnetcore.Infraestructure.TiposVinculo
{
    public class TipoVinculoEntityTypeConfiguration : IEntityTypeConfiguration<TipoVinculo>
    {
        public void Configure(EntityTypeBuilder<TipoVinculo> builder) 
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .HasConversion(
                    id => id.AsGuid(), 
                    guid => new TipoVinculoId(guid)); 

            builder.Property(b => b.Nome)
                .HasConversion(
                    b => b.Nome, 
                    b => new NomeVinculo(b)) 
                .IsRequired();
        }
    }
}