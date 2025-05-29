using System;

namespace dddnetcore.Domain.CargasMensais
{
    public class EditingCargaMensalDto
    {
        public double? JornadaDiaria { get; set; }
        public double? DiasUteisTrabalhaveis { get; set; }
        public double? FeriasBaixasLicencasFaltas { get; set; }
        public double? SalarioBase { get; set; }
        public double? TaxaSocialUnica { get; set; }


    }
}
