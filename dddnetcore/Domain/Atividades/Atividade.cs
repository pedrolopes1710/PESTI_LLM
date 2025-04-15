using System;
using dddnetcore.Domain.Atividades;
using dddnetcore.Domain.Tarefas;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Atividades
{
    public class Atividade : Entity<AtividadeId>, IAggregateRoot
    {
        public DataFimAtividade DataFimAtividade {get; private set;}
        public DataInicioAtividade DataInicioAtividade {get; private set;}
        public DescricaoAtividade DescricaoAtividade {get; private set;}
        public NomeAtividade NomeAtividade {get; private set;}

        public Tarefa Tarefa {get; private set;}
        
        private Atividade() {}

        public Atividade(
            DataFimAtividade dataFimAtividade,
            DataInicioAtividade dataInicioAtividade,
            DescricaoAtividade descricaoAtividade,
            NomeAtividade nomeAtividade,
            Tarefa tarefa)
        {
            this.Id = new AtividadeId(Guid.NewGuid());
            this.DataFimAtividade = dataFimAtividade;
            this.DataInicioAtividade = dataInicioAtividade;
            this.DescricaoAtividade = descricaoAtividade;
            this.NomeAtividade = nomeAtividade;
            this.Tarefa = tarefa;
        }

    }
}