using System;
using System.Collections.Generic;
using dddnetcore.Domain.Atividades;
using dddnetcore.Domain.Orcamentos;
using dddnetcore.Domain.Projetos;
using dddnetcore.Domain.Tarefas;
using dddnetcore.Domain.Entregaveis;
using DDDSample1.Domain.Shared;
using dddnetcore.Domain.Perfis;

namespace dddnetcore.Domain.Atividades
{
    public class Atividade : Entity<AtividadeId>, IAggregateRoot
    {
        public DataFimAtividade DataFimAtividade { get; private set; }
        public DataInicioAtividade DataInicioAtividade {get; private set;}
        public DescricaoAtividade DescricaoAtividade {get; private set;}
        public NomeAtividade NomeAtividade {get; private set;}

        public List<Tarefa> Tarefas {get; private set;}

        public List<Entregavel> Entregaveis {get; private set;}

        public List<Perfil> Perfis {get; private set;}  

        public List<Orcamento> Orcamentos {get;private set;}
        
        
        private Atividade() { }

        public Atividade(
            DataFimAtividade dataFimAtividade,
            DataInicioAtividade dataInicioAtividade,
            DescricaoAtividade descricaoAtividade,
            NomeAtividade nomeAtividade
            )
        {
            if (dataInicioAtividade == null)
                throw new BusinessRuleValidationException("DataInicioAtividade cannot be null.");

            if (dataFimAtividade == null)
                throw new BusinessRuleValidationException("DataFimAtividade cannot be null.");

            if (dataInicioAtividade.InicioAtividade == DateTime.MinValue)
                throw new BusinessRuleValidationException("DataInicioAtividade is invalid.");

            if (dataFimAtividade.FimAtividade < dataInicioAtividade.InicioAtividade)
                throw new BusinessRuleValidationException("DataFimAtividade cannot be earlier than DataInicioAtividade.");

            if (descricaoAtividade == null)
                throw new BusinessRuleValidationException("DescricaoAtividade cannot be null.");

            if (nomeAtividade == null)
                throw new BusinessRuleValidationException("NomeAtividade cannot be null.");

            this.Id = new AtividadeId(Guid.NewGuid());
            this.DataFimAtividade = dataFimAtividade;
            this.DataInicioAtividade = dataInicioAtividade;
            this.DescricaoAtividade = descricaoAtividade;
            this.NomeAtividade = nomeAtividade;

        }
        
        public void ChangeDataFimAtividade(DataFimAtividade dataFimAtividade)
        {
            if (dataFimAtividade == null)
            {
            throw new BusinessRuleValidationException("DataFimAtividade cannot be null.");
            }

            if (dataFimAtividade.FimAtividade < this.DataInicioAtividade.InicioAtividade)
            {
            throw new BusinessRuleValidationException("DataFimAtividade cannot be earlier than DataInicioAtividade.");
            }

            this.DataFimAtividade = dataFimAtividade;
        }

        public void ChangeDataInicioAtividade(DataInicioAtividade dataInicioAtividade)
        {
            if (dataInicioAtividade == null)
            {
                throw new BusinessRuleValidationException("DataInicioAtividade cannot be null.");
            }

            if (dataInicioAtividade.InicioAtividade == DateTime.MinValue)
            {
                throw new BusinessRuleValidationException("DataInicioAtividade cannot be DateTime.MinValue.");
            }

            if (this.DataFimAtividade != null && dataInicioAtividade.InicioAtividade > this.DataFimAtividade.FimAtividade)
            {
                throw new BusinessRuleValidationException("DataInicioAtividade cannot be later than DataFimAtividade.");
            }

            this.DataInicioAtividade = dataInicioAtividade;
        }

        public void ChangeDescricaoAtividade(DescricaoAtividade descricaoAtividade)
        {
            if (descricaoAtividade == null)
            {
            throw new BusinessRuleValidationException("DescricaoAtividade cannot be null.");
            }

            this.DescricaoAtividade = descricaoAtividade;
        }

        public void ChangeNomeAtividade(NomeAtividade nomeAtividade)
        {
            if (nomeAtividade == null)
            {
            throw new BusinessRuleValidationException("NomeAtividade cannot be null.");
            }

            this.NomeAtividade = nomeAtividade;
        }

        public void AddOrcamento(Orcamento orcamento)
        {
            if (orcamento == null)
            {
                throw new BusinessRuleValidationException("Orcamento cannot be null.");
            }

            if (this.Orcamentos == null)
            {
                this.Orcamentos = new List<Orcamento>();
            }

            this.Orcamentos.Add(orcamento);
        }

    }
}