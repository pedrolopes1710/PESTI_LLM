using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Contratos;
using dddnetcore.Domain.Pessoas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dddnetcore.Infraestructure.Pessoas
{
    public class PessoaEntityTypeConfiguration : IEntityTypeConfiguration<Pessoa>
    {
        public void Configure(EntityTypeBuilder<Pessoa> builder) {
            builder.HasKey(b => b.Id);
            builder.HasKey(b=>b.ContratoId);
            builder.Property(b => b.Nome).HasConversion(b => b.Nome, b => new NomePessoa(b)).IsRequired();
            builder.Property(b=>b.Email).HasConversion(b=>b.Email, b=>new EmailPessoa(b)).IsRequired();
            builder.Property(b => b.ContratoId).IsRequired();

            builder.HasOne<Contrato>().WithOne().HasForeignKey<Pessoa>(b => b.ContratoId).OnDelete(DeleteBehavior.Cascade);
        }
    }
}