using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dddnetcore.Domain.Orcamentos;
using dddnetcore.Domain.Atividades;

namespace dddnetcore.Infraestructure.Atividades
{
    public class AtividadeEntityTypeConfiguration : IEntityTypeConfiguration<Atividade>
    {
        public void Configure(EntityTypeBuilder<Atividade> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .HasConversion(
                    id => id.AsGuid(), 
                    guid => new AtividadeId(guid));
            builder.Property(b => b.DataInicioAtividade)
                .HasConversion(
                    b => b.InicioAtividade,
                    b => new DataInicioAtividade(b)).IsRequired();

            builder.Property(b => b.DataFimAtividade)
                .HasConversion(
                    b => b.FimAtividade,
                    b => new DataFimAtividade(b)).IsRequired();

            builder.Property(b => b.NomeAtividade)
                .HasConversion(
                    b => b.Nome,
                    b => new NomeAtividade(b)).IsRequired();
            
            
            builder.Property(b => b.DescricaoAtividade)
                .HasConversion(
                    b => b.Descricao,
                    b => new DescricaoAtividade(b)).IsRequired();
            

            builder.HasOne(b => b.Tarefa)
                .WithMany()
                .HasForeignKey("TarefaId")
                .IsRequired();
        }
    }
}