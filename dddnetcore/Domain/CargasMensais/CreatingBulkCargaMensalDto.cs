using System;

namespace dddnetcore.Domain.CargasMensais
{
    public class CreatingBulkCargaMensalDto
    {
        public double JornadaDiaria { get; set; }
        public double DiasUteisTrabalhaveis { get; set; }
        public double FeriasBaixasLicencasFaltas { get; set; }
        public DateTime MesAnoInicio { get; set; }
        public DateTime MesAnoFim { get; set; }
        public double SalarioBase { get; set; }
        public double TaxaSocialUnica { get; set; }
        public string PessoaId { get; set; }
    }
}
