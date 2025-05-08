using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Atividades;
using dddnetcore.Domain.Tarefas;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace dddnetcore.Infraestructure.Atividades
{
    public class AtividadeRepository : BaseRepository<Atividade,AtividadeId>,IAtividadeRepository
    {
        private readonly DDDSample1DbContext _context;
        
        public AtividadeRepository(DDDSample1DbContext context):base(context.Atividades) {
            _context = context;
        }

        public async Task<Atividade> UpdateAsync(Atividade atividade) {
            _context.Atividades.Update(atividade);

            await _context.SaveChangesAsync();

            return atividade;
        }

        public new async Task<Atividade> GetByIdAsync(AtividadeId id) {
            return await _context.Atividades
                .Include(a => a.Tarefas)
                .Include(a => a.Entregaveis)
                .Include(a => a.Perfis)
                .Include(a => a.Orcamentos)
                .FirstOrDefaultAsync(a => a.Id.Equals(id));
        }
    }
}