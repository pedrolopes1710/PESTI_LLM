using dddnetcore.Domain.Projetos;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dddnetcore.Infrastructure.Projetos
{
    public class ProjetoRepository : BaseRepository<Projeto, ProjetoId>, IProjetoRepository
    {
        private readonly DDDSample1DbContext _context;
        
        public ProjetoRepository(DDDSample1DbContext context) : base(context.Projetos)
        {
            _context = context;

        }

        public async Task<List<Projeto>> GetByIdsAsync(IEnumerable<ProjetoId> ids)
        {
            return await _context.Projetos
                .Where(p => ids.Contains(p.Id))
                .ToListAsync();
        }
    }
}