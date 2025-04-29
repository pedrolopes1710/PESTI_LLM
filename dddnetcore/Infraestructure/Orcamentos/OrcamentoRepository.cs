using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Orcamentos;
using dddnetcore.Domain.Rubricas;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace dddnetcore.Infraestructure.Orcamentos
{
    public class OrcamentoRepository : BaseRepository<Orcamento,OrcamentoId>,IOrcamentoRepository
    {
        private readonly DDDSample1DbContext _context;
        
        public OrcamentoRepository(DDDSample1DbContext context):base(context.Orcamentos) {
            _context = context;
        }

        public async Task<List<Orcamento>> GetOrcamentosAsync(Guid? rubricaId = null)
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
        }

        public new async Task<Orcamento> GetByIdAsync(OrcamentoId id) {
            return await _context.Orcamentos
                .Include(o => o.Rubrica)
                .FirstOrDefaultAsync(o => o.Id.Equals(id));
        }
        public new async Task<List<Orcamento>> GetAllAsync() {
            return await _context.Orcamentos
                .Include(o => o.Rubrica)
                .ToListAsync();
        }
    }
}