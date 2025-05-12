using System;
using System.Collections.Generic;
using dddnetcore.Domain.Atividades;
using dddnetcore.Domain.Despesas;
using dddnetcore.Domain.Rubricas;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Orcamentos
{
    public class Orcamento : Entity<OrcamentoId>, IAggregateRoot
    {
        public GastoPlaneado GastoPlaneado {get; private set;}
        public Rubrica Rubrica {get; private set;}
        public List<Despesa> Despesas {get; private set;}
        public AtividadeId AtividadeId {get; private set;}

        private Orcamento() {}

        public Orcamento(
            GastoPlaneado gastoPlaneado,
            Rubrica rubrica)
        {
            Id = new OrcamentoId(Guid.NewGuid());
            GastoPlaneado = gastoPlaneado;
            Rubrica = rubrica;
            Despesas = [];
        }

        public void MudarGastoPlaneado(GastoPlaneado gasto) {
            ArgumentNullException.ThrowIfNull(gasto);
            this.GastoPlaneado = gasto;
        }

        public void MudarRubrica(Rubrica rubrica) {
            ArgumentNullException.ThrowIfNull(rubrica);
            this.Rubrica = rubrica;
        } 
        public void SetAtividadeId(AtividadeId atividadeId)
        {
            if (atividadeId == null)
                throw new BusinessRuleValidationException("AtividadeId não pode ser nulo.");
            this.AtividadeId = atividadeId;
        }  

        public void AddDespesa(Despesa despesa) {
            ArgumentNullException.ThrowIfNull(despesa);
            this.Despesas.Add(despesa);
        }
    }
}