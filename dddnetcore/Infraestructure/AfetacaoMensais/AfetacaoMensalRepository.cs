using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Atividades;
using dddnetcore.Domain.Tarefas;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;
using dddnetcore.Infraestructure.AfetacaoPerfis;
using dddnetcore.Domain.AfetacaoPerfis;
using dddnetcore.Domain.AfetacaoMensais;
using dddnetcore.Domain.CargasMensais;

namespace dddnetcore.Infraestructure.AfetacaoPerfis
{
    public class AfetacaoMensalRepository : BaseRepository<AfetacaoMensal,AfetacaoMensalId>,IAfetacaoMensalRepository
    {
        private readonly DDDSample1DbContext _context;
        
        public AfetacaoMensalRepository(DDDSample1DbContext context):base(context.AfetacaoMensais) {
            _context = context;
        }

        public async Task<List<AfetacaoMensal>> GetByCargaMensalIdAsync(CargaMensalId cargaMensalId) {
            var query = _context.AfetacaoMensais.AsQueryable();

            query = query.Where(af => af.CargaMensal.Id.Equals(cargaMensalId));
            query = query.Include(af => af.AfetacaoPerfil);
            query = query.Include(af => af.CargaMensal);
            
            return await query.ToListAsync();
        }

        public new async Task<List<AfetacaoMensal>> GetAllAsync() {
            return await _context.AfetacaoMensais
                .Include(o => o.AfetacaoPerfil)
                .Include(o => o.CargaMensal)
                .ToListAsync();
        }

        public async Task<AfetacaoMensal> GetByIdAsync(AfetacaoMensalId id) {
            var query = _context.AfetacaoMensais.AsQueryable();

            query = query.Where(af => af.Id.Equals(id));
            query = query.Include(af => af.AfetacaoPerfil);
            query = query.Include(af => af.CargaMensal);

            return await query.FirstOrDefaultAsync();
        }

        public async Task<AfetacaoMensal> UpdateAsync(AfetacaoMensal afetacaoMensal) {
            _context.AfetacaoMensais.Update(afetacaoMensal);

            await _context.SaveChangesAsync();

            return afetacaoMensal;
        }
    }
}