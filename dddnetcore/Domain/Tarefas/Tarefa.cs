using System;
using dddnetcore.Domain.Atividades;
using dddnetcore.Domain.Rubricas;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Tarefas
{
    public class Tarefa : Entity<TarefaId>, IAggregateRoot
    {
        public NomeTarefa NomeTarefa {get; private set;}
        public DescricaoTarefa DescricaoTarefa {get; private set;}
        public StatusTarefa StatusTarefa {get; private set;}
        
        public AtividadeId AtividadeId { get; private set; } // já existe no DB
        
        private Tarefa() {}

        public Tarefa(NomeTarefa nomeTarefa, DescricaoTarefa descricaoTarefa, StatusTarefa statusTarefa)
        {
            this.Id = new TarefaId(Guid.NewGuid());
            this.DescricaoTarefa =descricaoTarefa;
            this.NomeTarefa = nomeTarefa;
            this.StatusTarefa = statusTarefa;
        }
        public void ChangeStatus(StatusTarefa statusTarefa)
        {
            if (statusTarefa.Equals(default(StatusTarefa)))
            {
            throw new BusinessRuleValidationException("Status da tarefa não pode ser o valor padrão.");
            }
            this.StatusTarefa = statusTarefa;
        }

        public void ChangeNome(NomeTarefa nomeTarefa)
        {
            if (nomeTarefa == null)
            {
            throw new BusinessRuleValidationException("Nome da tarefa não pode ser nulo.");
            }
            this.NomeTarefa = nomeTarefa;
        }

        public void ChangeDescricao(DescricaoTarefa descricaoTarefa)
        {
            if (descricaoTarefa == null)
            {
            throw new BusinessRuleValidationException("Descrição da tarefa não pode ser nula.");
            }
            this.DescricaoTarefa = descricaoTarefa;
        }
        public void SetAtividadeId(AtividadeId atividadeId)
        {
            if (atividadeId == null)
                throw new BusinessRuleValidationException("AtividadeId não pode ser nulo.");
            this.AtividadeId = atividadeId;
        }

               
    }
}