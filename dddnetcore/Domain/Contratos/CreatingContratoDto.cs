using System;

namespace dddnetcore.Domain.Contratos
{
    public class CreatingContratoDto
    {
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; } // Pode ser null
        public double Salario { get; set; }
        public string Tipo { get; set; } // Enum como string

        public CreatingContratoDto(DateTime dataInicio, DateTime dataFim, double salario, string tipo)
        {
            DataInicio = dataInicio;
            DataFim = dataFim;
            Salario = salario;
            Tipo = tipo;
        }
    }
}
