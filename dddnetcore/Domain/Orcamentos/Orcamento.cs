using System;
using System.Collections.Generic;
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

        private Orcamento() {}

        public Orcamento(
            GastoPlaneado gastoPlaneado,
            Rubrica rubrica,
            List<Despesa> despesas)
        {
            Id = new OrcamentoId(Guid.NewGuid());
            GastoPlaneado = gastoPlaneado;
            Rubrica = rubrica;
            Despesas = despesas;
        }

        public void MudarGastoPlaneado(GastoPlaneado gasto) {
            ArgumentNullException.ThrowIfNull(gasto);
            this.GastoPlaneado = gasto;
        }

        //TODO editar rubrica (ver TODO no serviço)
    }
}