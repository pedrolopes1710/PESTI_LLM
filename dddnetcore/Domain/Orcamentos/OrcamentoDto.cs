using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Despesas;
using dddnetcore.Domain.Rubricas;

namespace dddnetcore.Domain.Orcamentos
{
    public class OrcamentoDto
    {
        public Guid Id {get;set;}
        public double GastoPlaneado {get;set;}
        public RubricaDto Rubrica {get;set;}
        public List<DespesaDto> Despesas {get;set;}

        public OrcamentoDto() { }

        public OrcamentoDto(Orcamento orcamento)
        {
            this.Id = orcamento.Id.AsGuid();
            this.GastoPlaneado = orcamento.GastoPlaneado.Quantidade;
            this.Rubrica = new RubricaDto(orcamento.Rubrica);
            this.Despesas = orcamento.Despesas.Select(d => new DespesaDto(d)).ToList();
        }
    }
}