using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Despesas;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;

namespace dddnetcore.Infraestructure.Despesas
{
    public class DespesaRepository : BaseRepository<Despesa, DespesaId>, IDespesaRepository
    {
        private readonly DDDSample1DbContext _context;
        
        public DespesaRepository(DDDSample1DbContext context) : base(context.Despesas)
        {
            _context = context;
        }

        public async Task<Despesa> UpdateAsync(Despesa despesa)
        {
            _context.Despesas.Update(despesa);
            await _context.SaveChangesAsync();
            return despesa;
        }
    }
}