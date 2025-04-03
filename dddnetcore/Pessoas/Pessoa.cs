using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Pessoas
{
    public class Pessoa : Entity<PessoaId>, IAggregateRoot
    {
        public NomePessoa Nome {get; private set;}
        public EmailPessoa Email {get; private set;}
        public CienciaId Ciencia{get;private set;}

        private Pessoa() {
        }

        public Pessoa(string nome, string email, string cienciaId) {
            this.Id = new PessoaId(Guid.NewGuid());
            this.Nome = new NomePessoa(nome);
            this.Email = new EmailPessoa(email);
            this.Ciencia = new CienciaId(cienciaId);
        }
    }
}