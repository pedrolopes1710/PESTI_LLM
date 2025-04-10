using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dddnetcore.Domain.Indicadores;

namespace dddnetcore.Infrastructure.Indicadores
{
    internal class IndicadorEntityTypeConfiguration : IEntityTypeConfiguration<Indicador>
    {
        public void Configure(EntityTypeBuilder<Indicador> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .HasConversion(
                    id => id.AsGuid(),
                    guid => new IndicadorId(guid));

            builder.OwnsOne(b => b.Nome)
                .Property(p => p.Valor)
                .HasColumnName("NomeIndicador")
                .IsRequired();

            builder.OwnsOne(b => b.ValorAtual)
                .Property(p => p.Valor)
                .HasColumnName("ValorAtual")
                .IsRequired();

            builder.OwnsOne(b => b.ValorMaximo)
                .Property(p => p.Valor)
                .HasColumnName("ValorMaximo")
                .IsRequired();
        }
    }
}