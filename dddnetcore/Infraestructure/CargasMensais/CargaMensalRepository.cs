using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.CargasMensais;
using dddnetcore.Domain.Pessoas;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace dddnetcore.Infraestructure.CargasMensais
{
    public class CargaMensalRepository : BaseRepository<CargaMensal, CargaMensalId>, ICargaMensalRepository
    {
        private readonly DDDSample1DbContext _context;

        public CargaMensalRepository(DDDSample1DbContext context) : base(context.CargasMensais)
        {
            _context = context;

        }

        public async Task<CargaMensal> GetByMesAnoAndPessoaAsync(MesAno mesAno, PessoaId pessoaId)
        {
            return await _context.CargasMensais
                .Where(c => c.MesAno.Equals(mesAno) && c.PessoaId.Equals(pessoaId))
                .FirstOrDefaultAsync();
        }

        public async Task<List<CargaMensal>> GetByPessoaIdAsync(PessoaId pessoaId)
        {
            return await _context.CargasMensais
                .Where(c => c.PessoaId == pessoaId)
                .ToListAsync();
        }

        public async Task<CargaMensal> UpdateAsync(CargaMensal cargaMensal)
        {
            _context.CargasMensais.Update(cargaMensal);
            await _context.SaveChangesAsync();
            return cargaMensal;
        }

    }
}