using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Contratos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace dddnetcore.Infraestructure.Contratos
{
    public class ContratoEntityTypeConfiguration : IEntityTypeConfiguration<Contrato>
    {
        public void Configure(EntityTypeBuilder<Contrato> builder) {
            builder.HasKey(b => b.Id);
            builder.Property(b=>b.DataInicioContrato).HasConversion(b=>b.InicioContrato,b=>new DataInicioContrato(b)).IsRequired();
            builder.Property(b => b.DataFimContrato).HasConversion(b => b.FimContrato, b => new DataFimContrato(b)).IsRequired();
            builder.Property(b=>b.TipoContrato).HasConversion(new EnumToStringConverter<TipoContrato>()).IsRequired();
        }
    }
}