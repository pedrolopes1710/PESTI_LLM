using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dddnetcore.Domain.Perfis;
using dddnetcore.Domain.Projetos;

namespace dddnetcore.Infrastructure.Perfis
{
    internal class PerfilEntityTypeConfiguration : IEntityTypeConfiguration<Perfil>
    {
        public void Configure(EntityTypeBuilder<Perfil> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .HasConversion(
                    id => id.AsGuid(),
                    guid => new PerfilId(guid))
                .ValueGeneratedNever();

            builder.OwnsOne(b => b.DescricaoPerfil, dp =>
            {
                dp.Property(p => p.Valor)
                    .HasColumnName("DescricaoPerfil")
                    .IsRequired();
            });

            builder.OwnsOne(b => b.PMs, pm =>
            {
                pm.Property(p => p.Valor)
                    .HasColumnName("PMsTotais")
                    .IsRequired();
            });
            
            builder.HasOne<Projeto>()
                .WithMany(p => p.Perfis)
                .HasForeignKey("ProjetoId")
                .IsRequired();
            
            builder.Navigation(b => b.DescricaoPerfil).IsRequired();
            builder.Navigation(b => b.PMs).IsRequired();
        }
    }
}