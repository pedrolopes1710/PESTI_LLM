using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using dddnetcore.Domain.Orcamentos;
using dddnetcore.Domain.Atividades;
using dddnetcore.Domain.Projetos;

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
            
            builder.HasOne<Projeto>()
                .WithMany(p => p.Atividades)
                .HasForeignKey("ProjetoId")
                .IsRequired(false);

            builder.HasMany(p => p.Tarefas)
                .WithOne()
                .HasForeignKey("AtividadeId");
                
            builder.HasMany(p => p.Orcamentos)
                .WithOne()
                .HasForeignKey("AtividadeId");

            builder.HasMany(p => p.Entregaveis)
                .WithOne()
                .HasForeignKey("AtividadeId");

            builder.HasMany(p => p.Perfis)
                .WithOne()
                .HasForeignKey("AtividadeId");
              
        }
    }
}