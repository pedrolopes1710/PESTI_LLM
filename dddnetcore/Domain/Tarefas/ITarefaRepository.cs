using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Tarefas
{
    public interface ITarefaRepository : IRepository<Tarefa, TarefaId>
    {        
        public Task<Tarefa> UpdateAsync(Tarefa tarefa);
    }
}
