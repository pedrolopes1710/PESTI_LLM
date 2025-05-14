using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Pessoas;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace dddnetcore.Infraestructure.Orcamentos
{
    public class PessoaRepository : BaseRepository<Pessoa,PessoaId>,IPessoaRepository
    {
        private readonly DDDSample1DbContext _context;
        
        public PessoaRepository(DDDSample1DbContext context):base(context.Pessoas) {
            _context = context;
        }

        public new async Task<Pessoa> GetByIdAsync(PessoaId id) {
            return await _context.Pessoas
                .Include(p => p.Contrato)
                .Include(p => p.CargasMensais)
                .Include(p => p.Projetos) //talvez esta não faça sentido
                .FirstOrDefaultAsync(p => p.Id.Equals(id));
        }   
    }
}