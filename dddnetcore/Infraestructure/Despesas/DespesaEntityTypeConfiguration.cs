using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.CargasMensais;
using dddnetcore.Domain.Despesas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dddnetcore.Infraestructure.Despesas
{
    public class DespesaEntityTypeConfiguration : IEntityTypeConfiguration<Despesa>
    {
        public void Configure(EntityTypeBuilder<Despesa> builder) 
        {
            builder.HasKey(b => b.Id);

            builder.Property(b => b.Id)
                .HasConversion(
                    id => id.AsGuid(), 
                    guid => new DespesaId(guid)); 

            builder.Property(b => b.Valor)
                .HasConversion(
                    b => b.Valor, 
                    b => new ValorDespesa(b)) 
                .IsRequired();

            builder.Property(b => b.Descricao)
                .HasConversion(
                    b => b.Descricao, 
                    b => new DescricaoDespesa(b)) 
                .IsRequired();

            builder.HasOne(b => b.CargaMensal)
                .WithOne()
                .HasForeignKey<Despesa>(b => b.CargaMensalId)
                .IsRequired(false);
        }
    }
}