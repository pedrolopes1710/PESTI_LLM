using System;

#nullable enable
using dddnetcore.Domain.CargasMensais;

namespace dddnetcore.Domain.Despesas
{
    public class DespesaDto
    {
        public Guid Id { get; set; }
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public CargaMensalDto? CargaMensal { get; set; }

        public DespesaDto(Despesa despesa) { 
            this.Id = despesa.Id.AsGuid(); 
            this.Descricao = despesa.Descricao.Descricao;
            this.Valor = despesa.Valor.Valor;
            this.CargaMensal = despesa.CargaMensal != null ? new CargaMensalDto(despesa.CargaMensal) : null;
        }
    }
}