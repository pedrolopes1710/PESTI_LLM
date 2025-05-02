using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dddnetcore.Infraestructure.AfetacaoPerfis;
using dddnetcore.Domain.AfetacaoMensais;

namespace dddnetcore.Infraestructure.AfetacaoMensais
{
    public class AfetacaoMensalEntityTypeConfiguration : IEntityTypeConfiguration<AfetacaoMensal>
    {
        public void Configure(EntityTypeBuilder<AfetacaoMensal> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .HasConversion(
                    id => id.AsGuid(), 
                    guid => new AfetacaoMensalId(guid));
            
            builder.Property(b => b.PMs)
                .HasConversion(
                    b => b.Quantidade,
                    b => new PMs(b)).IsRequired();

            builder.HasOne(b => b.AfetacaoPerfil)
                .WithMany()
                .HasForeignKey("AfetacaoPerfilId")
                .IsRequired();

            builder.HasOne(b => b.CargaMensal)
                .WithMany()
                .HasForeignKey("CargaMensalId")
                .IsRequired();
        }
    }
}