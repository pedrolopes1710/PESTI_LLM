using System;
using dddnetcore.Domain.CargasMensais;
using dddnetcore.Domain.Pessoas;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace dddnetcore.Infraestructure.CargasMensais
{
    public class CargaMensalEntityTypeConfiguration : IEntityTypeConfiguration<CargaMensal>
    {
        public void Configure(EntityTypeBuilder<CargaMensal> builder)
        {
            builder.HasKey(c => c.Id);

            builder.Property(c => c.Id)
                .HasConversion(
                    id => id.AsGuid(),
                    guid => new CargaMensalId(guid));

            builder.Property(c => c.JornadaDiaria)
                .HasConversion(
                    v => v.Valor,
                    v => new JornadaDiaria(v)
                )
                .IsRequired();

            builder.Property(c => c.DiasUteis)
                .HasConversion(
                    v => v.Valor,
                    v => new DiasUteisTrabalhaveis(v)
                )
                .IsRequired();

            builder.Property(c => c.Ausencias)
                .HasConversion(
                    v => v.Dias,
                    v => new FeriasBaixasLicencasFaltas(v)
                )
                .IsRequired();

            builder.Property(c => c.SalarioBase)
                .HasConversion(
                    v => v.Valor,
                    v => new SalarioBase(v)
                )
                .IsRequired();

            builder.Property(c => c.MesAno)
                .HasConversion(
                    v => v.Valor,
                    v => new MesAno(v)
                )
                .IsRequired();

            builder.Property(c => c.TSU)
                .HasConversion(
                    v => v.Valor,
                    v => new TaxaSocialUnica(v)
                )
                .IsRequired();

            /*builder.HasOne<Pessoa>()
                .WithMany(p => p.CargasMensais)
                .HasForeignKey("PessoaId")
                .IsRequired(false);*/
        }
    }
}
