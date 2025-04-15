using System;

namespace dddnetcore.Domain.Contratos
{
    public class ContratoDto
    {
        public string Id { get; set; }
        public string Tipo { get; set; } // Nome do enum como string (ex: "Investigador")
        public double Salario { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime? DataFim { get; set; } // Nullable para contratos sem fim definido
        public bool Ativo { get; set; }
    }
}
