using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Entregaveis;
using dddnetcore.Domain.Orcamentos;
using dddnetcore.Domain.Rubricas;
using dddnetcore.Domain.Tarefas;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace dddnetcore.Infraestructure.Entregaveis
{
    public class EntregavelRepository : BaseRepository<Entregavel,EntregavelId>,IEntregavelRepository
    {
        private readonly DDDSample1DbContext _context;
        
        public EntregavelRepository(DDDSample1DbContext context):base(context.Entregaveis) {
            _context = context;
        }

        public async Task<Entregavel> UpdateAsync(Entregavel entregavel) {
            _context.Entregaveis.Update(entregavel);

            await _context.SaveChangesAsync();

            return entregavel;
        }
    }
}