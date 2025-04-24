using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.TiposEntregavel
{
    public class TipoEntregavel : Entity<TipoEntregavelId>, IAggregateRoot
    {
        public NomeTipoEntregavel Nome {get; private set;}

        private TipoEntregavel() {
        }

        public TipoEntregavel(string nome) {
            this.Id = new TipoEntregavelId(Guid.NewGuid());
            this.Nome = new NomeTipoEntregavel(nome);
        }

        public void AlterarAtributos(string nome){
            this.Nome = new NomeTipoEntregavel(nome);
        }
    }
}