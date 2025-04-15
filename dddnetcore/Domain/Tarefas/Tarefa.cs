using System;
using dddnetcore.Domain.Rubricas;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Tarefas
{
    public class Tarefa : Entity<TarefaId>, IAggregateRoot
    {
        public NomeTarefa NomeTarefa {get; private set;}
        public DescricaoTarefa DescricaoTarefa {get; private set;}
        public StatusTarefa StatusTarefa {get; private set;}

        private Tarefa() {}

        public Tarefa(NomeTarefa nomeTarefa, DescricaoTarefa descricaoTarefa, StatusTarefa statusTarefa)
        {
            this.Id = new TarefaId(Guid.NewGuid());
            this.DescricaoTarefa =descricaoTarefa;
            this.NomeTarefa = nomeTarefa;
            this.StatusTarefa = statusTarefa;
        }
    }
}