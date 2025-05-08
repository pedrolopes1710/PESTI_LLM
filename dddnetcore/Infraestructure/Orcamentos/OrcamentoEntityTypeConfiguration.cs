using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dddnetcore.Domain.Orcamentos;
using dddnetcore.Domain.Atividades;

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

            builder.HasOne(b => b.Rubrica)
                .WithMany()
                .HasForeignKey("RubricaId")
                .IsRequired();

            builder.HasMany(b => b.Despesas)
                .WithOne()
                .HasForeignKey("OrcamentoId")
                .OnDelete(DeleteBehavior.Cascade)
                .IsRequired();
            
            builder.HasOne<Atividade>()
                .WithMany(p => p.Orcamentos)
                .HasForeignKey("AtividadeId")
                .IsRequired(false);
        }
    }
}