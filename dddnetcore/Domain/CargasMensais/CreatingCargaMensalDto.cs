using System;

namespace dddnetcore.Domain.CargasMensais
{
    public class CreatingCargaMensalDto
    {
        public double JornadaDiaria { get; set; }
        public double DiasUteisTrabalhaveis { get; set; }
        public double FeriasBaixasLicencasFaltas { get; set; }
        public DateTime MesAno { get; set; }
        public double SalarioBase { get; set; }

    }
}
