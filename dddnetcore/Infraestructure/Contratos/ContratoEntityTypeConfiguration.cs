using System;
using dddnetcore.Domain.Contratos;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace dddnetcore.Infraestructure.Contratos
{
    public class ContratoEntityTypeConfiguration : IEntityTypeConfiguration<Contrato>
    {
        public void Configure(EntityTypeBuilder<Contrato> builder)
        {
            builder.HasKey(c => c.Id);
            
            builder.Property(b => b.Id)
                .HasConversion(
                    id => id.AsGuid(), 
                    guid => new ContratoId(guid));

            builder.Property(c => c.DataInicio)
                .HasConversion(
                    v => v.InicioContrato,
                    v => new DataInicioContrato(v)
                ).IsRequired();

            builder.Property(c => c.DataFim)
                .HasConversion(
                    v => v.FimContrato,
                    v => new DataFimContrato(v)
                );

            builder.Property(c => c.Salario)
                .HasConversion(
                    v => v.Valor,
                    v => new SalarioMensalContrato(v)
                ).IsRequired();

            builder.Property(c => c.Tipo)
                .HasConversion(new EnumToStringConverter<TipoContrato>())
                .IsRequired();

            builder.Property(c => c.Ativo)
                .IsRequired();
        }
    }
}
