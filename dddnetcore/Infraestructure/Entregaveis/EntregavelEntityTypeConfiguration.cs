using dddnetcore.Domain.Entregaveis;
using dddnetcore.Domain.TiposEntregavel;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dddnetcore.Infraestructure.Entregaveis
{
    public class EntregavelEntityTypeConfiguration : IEntityTypeConfiguration<Entregavel>
    {
        public void Configure(EntityTypeBuilder<Entregavel> builder) 
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .HasConversion(
                    id => id.AsGuid(), 
                    guid => new EntregavelId(guid)); 

            builder.Property(b => b.Nome)
                .HasConversion(
                    b => b.Nome, 
                    b => new NomeEntegavel(b)) 
                .IsRequired();

            builder.Property(b => b.Descricao)
                .HasConversion(
                    b => b.Descricao, 
                    b => new DescricaoEntregavel(b)) 
                .IsRequired();
            builder.Property(b=> b.Data)
                .HasConversion(
                    b => b.Data, 
                    b => new DataEntregavel(b)) 
                .IsRequired();
            builder.HasOne(b => b.TipoEntregavel)
                .WithMany()
                .HasForeignKey("TipoEntregavelId")
                .IsRequired(); 
        }
    }
}