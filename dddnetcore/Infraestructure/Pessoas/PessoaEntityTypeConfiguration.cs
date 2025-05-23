using System;
using dddnetcore.Domain.Pessoas;
using dddnetcore.Domain.CargasMensais;
using dddnetcore.Domain.Contratos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dddnetcore.Infraestructure.Pessoas
{
    public class PessoaEntityTypeConfiguration : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.Id)
                .HasConversion(
                    id => id.AsGuid(),
                    guid => new PessoaId(guid));

            builder.Property(p => p.Nome)
                .HasConversion(
                    nome => nome.Value,
                    nome => new NomePessoa(nome))
                .IsRequired();

            builder.Property(p => p.Email)
                .HasConversion(
                    email => email.Value,
                    email => new EmailPessoa(email))
                .IsRequired();

            builder.Property(p => p.CienciaId)
                .HasConversion(
                    c => c.Value,
                    c => new PessoaCienciaId(c))
                .IsRequired();

            builder.Property(p => p.UltimoPedidoPagamento)
                .HasConversion(
                    u => u.Value,
                    u => new PessoaUltimoPedPagam(u))
                .IsRequired();

            // Relação 1:N entre Pessoa e CargaMensal
            builder.HasMany(p => p.CargasMensais)
                .WithOne()
                .HasForeignKey("PessoaId");

            builder.HasOne(p => p.Contrato)
                .WithOne()
                .HasForeignKey<Pessoa>(p => p.ContratoId)
                .IsRequired(false);

            builder.Property(p => p.Ativo)
                .IsRequired()
                .HasDefaultValue(true);
    

            // Garante que apenas uma pessoa pode ter um contrato
            builder.HasIndex(p => p.ContratoId).IsUnique();
            
            builder.HasIndex(p => p.CienciaId).IsUnique();


   
        }
    }
}
 