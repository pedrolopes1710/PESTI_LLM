using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Rubricas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dddnetcore.Infraestructure.Rubricas
{
    public class RubricaEntityTypeConfiguration : IEntityTypeConfiguration<Rubrica>
    {
        public void Configure(EntityTypeBuilder<Rubrica> builder) {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Nome).HasConversion(b => b.Nome, b => new NomeRubrica(b)).IsRequired();
        }
    }
}