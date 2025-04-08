using System;
using dddnetcore.Domain.Contratos;
using dddnetcore.Domain.TiposVinculo;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.TiposVinculo
{
    public class TipoVinculo : Entity<TipoVinculoId>, IAggregateRoot
    {
        public NomeVinculo Nome {get; private set;}

        private TipoVinculo() {
        }

        public TipoVinculo(string nome) {
            this.Id = new TipoVinculoId(Guid.NewGuid());
            this.Nome = new NomeVinculo(nome);
        }
    }
}