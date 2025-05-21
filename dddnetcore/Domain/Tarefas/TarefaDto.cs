using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dddnetcore.Domain.Tarefas
{
    public class TarefaDto
    {
        public Guid Id {get;set;}
        public string Nome {get;set;}

        public string Status{get;set;}
        
        public string Descricao{get;set;}

        public Guid? AtividadeId { get; set; } // já existe no DB

        public TarefaDto() {}

        public TarefaDto(Tarefa tarefa)
        {
            this.Id = tarefa.Id.AsGuid();
            this.Nome = tarefa.NomeTarefa.Nome;
            this.Status=tarefa.StatusTarefa.ToString();
            this.Descricao=tarefa.DescricaoTarefa.Descricao;
            this.AtividadeId = tarefa.AtividadeId?.AsGuid();
        }   
    }
}