using System;

namespace dddnetcore.Domain.CargasMensais
{
    public class CargaMensalDto
    {
        public string Id { get; set; }
        public double JornadaDiaria { get; set; } // Nome do enum como string (ex: "Investigador")
        public double DiasUteisTrabalhaveis { get; set; }
        public double FeriasBaixasLicencasFaltas { get; set; }
        public DateTime? MesAno { get; set; } // Nullable para contratos sem fim definido
        public double SalarioBase { get; set; }


        public CargaMensalDto() {}

        public CargaMensalDto(CargaMensal cargaMensal)
        {
            Id = cargaMensal.Id.AsString();
            JornadaDiaria = cargaMensal.JornadaDiaria.Valor;        //
            DiasUteisTrabalhaveis = cargaMensal.DiasUteis.Valor;
            FeriasBaixasLicencasFaltas = cargaMensal.Ausencias.Dias;
            MesAno = cargaMensal.MesAno.Valor;
            SalarioBase = cargaMensal.SalarioBase.Valor;
        }
    }
}
