using System;
using System.Collections.Generic;
using dddnetcore.Domain.Atividades;
using dddnetcore.Domain.Indicadores;
using dddnetcore.Domain.Perfis;
using dddnetcore.Domain.Pessoas;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Projetos
{
    public class Projeto : Entity<ProjetoId>, IAggregateRoot
    {
        public NomeProjeto NomeProjeto { get; private set; }
        public DescricaoProjeto DescricaoProjeto { get; private set; }

        public List<Atividade> Atividades { get; private set; }
        public List<Perfil> Perfis { get; private set; }
        public List<Indicador> Indicadores { get; private set; }
        public ICollection<Pessoa> Pessoas { get; set; } = new List<Pessoa>();


        private Projeto()
        {
        }

        public Projeto(string nome, string descricao)
        {
            Id = new ProjetoId(Guid.NewGuid());
            NomeProjeto = new NomeProjeto(nome);
            DescricaoProjeto = new DescricaoProjeto(descricao);
            Atividades = new List<Atividade>();
            Perfis = new List<Perfil>();
            Indicadores = new List<Indicador>();
        }

        public void AlterarNome(string novoNome)
        {
            if (!string.IsNullOrWhiteSpace(novoNome))
                NomeProjeto = new NomeProjeto(novoNome);
        }

        public void AlterarDescricao(string novaDescricao)
        {
            if (!string.IsNullOrWhiteSpace(novaDescricao))
                DescricaoProjeto = new DescricaoProjeto(novaDescricao);
        }
    }
}