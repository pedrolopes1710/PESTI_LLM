using System;

namespace dddnetcore.Domain.Contratos
{
    public class EditingContratoDto
    {
        public string Id { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataFim { get; set; }
        public double Salario { get; set; }
        public string Tipo { get; set; }
        public bool Ativo { get; set; }

        public EditingContratoDto(string id, DateTime dataInicio, DateTime dataFim, double salario, string tipo, bool ativo)
        {
            Id = id;
            DataInicio = dataInicio;
            DataFim = dataFim;
            Salario = salario;
            Tipo = tipo;
            Ativo = ativo;
        }
    }
}
