using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Orcamentos;
using dddnetcore.Domain.Rubricas;
using dddnetcore.Domain.Tarefas;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace dddnetcore.Infraestructure.Tarefas
{
    public class TarefaRepository : BaseRepository<Tarefa,TarefaId>,ITarefaRepository
    {
        private readonly DDDSample1DbContext _context;
        
        public TarefaRepository(DDDSample1DbContext context):base(context.Tarefas) {
            _context = context;
        }

        public async Task<Tarefa> UpdateAsync(Tarefa tarefa) {
            _context.Tarefas.Update(tarefa);

            await _context.SaveChangesAsync();

            return tarefa   ;
        }
    }
}