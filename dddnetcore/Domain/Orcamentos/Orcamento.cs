using System;
using dddnetcore.Domain.Rubricas;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Orcamentos
{
    public class Orcamento : Entity<OrcamentoId>, IAggregateRoot
    {
        public GastoPlaneado GastoPlaneado {get; private set;}
        public GastoExecutado GastoExecutado {get; private set;}
        public Rubrica Rubrica {get; private set;}

        private Orcamento() {}

        public Orcamento(
            GastoPlaneado gastoPlaneado,
            GastoExecutado GastoExecutado,
            Rubrica Rubrica)
        {
            this.Id = new OrcamentoId(Guid.NewGuid());
            this.GastoPlaneado = gastoPlaneado;
            this.GastoExecutado = GastoExecutado;
            this.Rubrica = Rubrica;
        }

        public void MudarGastoExecutado(GastoExecutado gasto) {
            ArgumentNullException.ThrowIfNull(gasto);
            this.GastoExecutado = gasto;
        }

        public void MudarGastoExecutado(GastoPlaneado gasto) {
            ArgumentNullException.ThrowIfNull(gasto);
            this.GastoPlaneado = gasto;
        }

        //TODO editar rubrica (ver TODO no serviço)
    }
}