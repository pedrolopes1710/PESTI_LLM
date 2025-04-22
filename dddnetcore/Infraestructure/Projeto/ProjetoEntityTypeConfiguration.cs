using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dddnetcore.Domain.Projetos;

namespace dddnetcore.Infrastructure.Projetos
{
    internal class ProjetoEntityTypeConfiguration : IEntityTypeConfiguration<Projeto>
    {
        public void Configure(EntityTypeBuilder<Projeto> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasConversion(id => id.AsGuid(), guid => new ProjetoId(guid));

            builder.OwnsOne(p => p.NomeProjeto, np =>
            {
                np.Property(n => n.Valor)
                    .HasColumnName("NomeProjeto")
                    .IsRequired();
            });

            builder.OwnsOne(p => p.DescricaoProjeto, dp =>
            {
                dp.Property(d => d.Valor)
                    .HasColumnName("DescricaoProjeto")
                    .IsRequired();
            });
            
            builder.HasMany(p => p.Atividades)
                .WithOne()
                .HasForeignKey("ProjetoId");

            builder.HasMany(p => p.Perfis)
                .WithOne()
                .HasForeignKey("ProjetoId");
            

            builder.HasMany(p => p.Indicadores)
                .WithOne()
                .HasForeignKey("ProjetoId");
        }
    }
}