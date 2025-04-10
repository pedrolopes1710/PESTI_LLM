using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Projetos
{
    public class Projeto : Entity<ProjetoId>, IAggregateRoot
    {
        public NomeProjeto NomeProjeto { get; private set; }
        public DescricaoProjeto DescricaoProjeto { get; private set; }

        private Projeto() { }

        public Projeto(string nome, string descricao)
        {
            Id = new ProjetoId(Guid.NewGuid());
            NomeProjeto = new NomeProjeto(nome);
            DescricaoProjeto = new DescricaoProjeto(descricao);
        }
    }
}