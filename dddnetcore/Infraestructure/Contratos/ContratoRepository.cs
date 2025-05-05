using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Contratos;
using dddnetcore.Domain.Pessoas;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;


namespace dddnetcore.Infraestructure.Contratos
{
    public class ContratoRepository : BaseRepository<Contrato,ContratoId>,IContratoRepository
    {
        private readonly DDDSample1DbContext _context;

        public ContratoRepository(DDDSample1DbContext context):base(context.Contratos)
        {
            _context = context;
           
        }

        public async Task<Contrato> GetByPessoaIdAsync(PessoaId pessoaId)
        {
            return await _context.Pessoas
                .Where(p => p.Id == pessoaId && p.ContratoId != null)
                .Select(p => p.Contrato)
                .FirstOrDefaultAsync();
        }

    }
}