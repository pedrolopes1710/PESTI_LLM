using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dddnetcore.Domain.Orcamentos;

namespace dddnetcore.Infraestructure.Orcamentos
{
    public class OrcamentoEntityTypeConfiguration : IEntityTypeConfiguration<Orcamento>
    {
        public void Configure(EntityTypeBuilder<Orcamento> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .HasConversion(
                    id => id.AsGuid(), 
                    guid => new OrcamentoId(guid));

            builder.Property(b => b.GastoPlaneado)
                .HasConversion(
                    b => b.Quantidade,
                    b => new GastoPlaneado(b)).IsRequired();

            builder.Property(b => b.GastoExecutado)
                .HasConversion(
                    b => b.Quantidade,
                    b => new GastoExecutado(b)).IsRequired();

            builder.HasOne(b => b.Rubrica)
                .WithMany()
                .HasForeignKey("RubricaId")
                .IsRequired();
        }
    }
}