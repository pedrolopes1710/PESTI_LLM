using System;
using dddnetcore.Domain.TiposEntregavel;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Entregaveis
{
    public class Entregavel : Entity<EntregavelId>, IAggregateRoot
    {
        public NomeEntegavel Nome {get; private set;}

        public DescricaoEntregavel Descricao{get;private set;}

        public DataEntregavel Data{get;private set;}

        public TipoEntregavel TipoEntregavel{get;private set;}

        private Entregavel() {
        }

        public Entregavel(string nome, string descricao, DateTime data, TipoEntregavel tipo) {
            this.Id = new EntregavelId(Guid.NewGuid());
            this.Nome = new NomeEntegavel(nome);
            this.Descricao = new DescricaoEntregavel(descricao);
            this.Data = new DataEntregavel(data);
            this.TipoEntregavel = tipo; 
        }
    }
}