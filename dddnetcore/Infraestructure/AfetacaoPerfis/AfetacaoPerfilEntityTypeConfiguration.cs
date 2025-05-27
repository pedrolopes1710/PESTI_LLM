using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dddnetcore.Infraestructure.AfetacaoPerfis;
using dddnetcore.Domain.AfetacaoPerfis;

namespace dddnetcore.Infraestructure.AfetacaoPerfis
{
    public class AfetacaoPerfilEntityTypeConfiguration : IEntityTypeConfiguration<AfetacaoPerfil>
    {
        public void Configure(EntityTypeBuilder<AfetacaoPerfil> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .HasConversion(
                    id => id.AsGuid(), 
                    guid => new AfetacaoPerfilId(guid));
            
            builder.Property(b => b.DuracaoMes)
                .HasConversion(
                    b => b.Quantidade,
                    b => new DuracaoMes(b)).IsRequired();

            builder.Property(b => b.PMsAprovados)
                .HasConversion(
                    b => b.Quantidade,
                    b => new PMsAprovados(b)).IsRequired();

            builder.HasOne(b => b.Perfil)
                .WithMany()
                .HasForeignKey("PerfilId");

            builder.HasOne(b => b.Pessoa)
                .WithMany()
                .HasForeignKey("PessoaId");
                
        }
    }
}