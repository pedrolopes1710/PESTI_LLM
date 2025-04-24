using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dddnetcore.Domain.Orcamentos;
using dddnetcore.Domain.Tarefas;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using dddnetcore.Domain.Atividades;


namespace dddnetcore.Infraestructure.Tarefas
{
    public class TarefaEntityTypeConfiguration : IEntityTypeConfiguration<Tarefa>
    {
        public void Configure(EntityTypeBuilder<Tarefa> builder)
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .HasConversion(
                    id => id.AsGuid(), 
                    guid => new TarefaId(guid));

            builder.Property(b => b.NomeTarefa)
                .HasConversion(
                    b => b.Nome,
                    b => new NomeTarefa(b)).IsRequired();

            builder.Property(b => b.DescricaoTarefa)
                .HasConversion(
                    b => b.Descricao,
                    b => new DescricaoTarefa(b)).IsRequired();      

            builder.Property(b=>b.StatusTarefa).HasConversion(new EnumToStringConverter<StatusTarefa>()).IsRequired();
            
            builder.HasOne<Atividade>()
                .WithMany(p => p.Tarefas)
                .HasForeignKey("AtividadeId")
                .IsRequired(false);
        }
    }
}
