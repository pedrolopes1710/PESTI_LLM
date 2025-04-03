using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Rubricas
{
    public class Rubrica : Entity<RubricaId>, IAggregateRoot
    {
        public NomeRubrica Nome {get; private set;}

        private Rubrica() {
        }

        public Rubrica(string nome) {
            this.Id = new RubricaId(Guid.NewGuid());
            this.Nome = new NomeRubrica(nome);
        }
    }
}