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

        /*public async Task<List<Orcamento>> GetOrcamentosAsync(Guid? rubricaId = null)
        {
            if (rubricaId == null) return await GetAllAsync();

            var query = _context.Orcamentos.AsQueryable();

            query = query.Where(orcamento => orcamento.Rubrica.Id.Equals(new RubricaId(rubricaId.Value)));

            return await query.ToListAsync();
        }

        public async Task<Orcamento> UpdateAsync(Orcamento orcamento) {
            _context.Orcamentos.Update(orcamento);

            await _context.SaveChangesAsync();

            return orcamento;
        }*/
    }
}