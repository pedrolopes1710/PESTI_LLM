using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Rubricas;
using dddnetcore.Domain.Orcamentos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dddnetcore.Infraestructure.Orcamentos
{
    public class OrcamentoEntityTypeConfiguration : IEntityTypeConfiguration<Orcamento>
    {
        public void Configure(EntityTypeBuilder<Orcamento> builder) {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.GastoPlaneado).HasConversion(b => b.Quantidade, b => new GastoPlaneado(b)).IsRequired();
            builder.Property(b => b.GastoExecutado).HasConversion(b => b.Quantidade, b => new GastoExecutado(b)).IsRequired();
            builder.HasOne(b => b.Rubrica).WithMany().HasForeignKey("RubricaId").IsRequired();
        }
    }
}