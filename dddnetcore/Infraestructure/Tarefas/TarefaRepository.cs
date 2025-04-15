using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Tarefas;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace dddnetcore.Infraestructure.Tarefas
{
    public class TarefaRepository : BaseRepository<Tarefa,TarefaId>,ITarefaRepository
    {
    
        
        public TarefaRepository(DDDSample1DbContext context):base(context.Tarefas) {
        
        }
    }
}