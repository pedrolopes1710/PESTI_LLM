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

namespace dddnetcore.Infraestructure.AfetacaoPerfis
{
    public class AfetacaoMensalRepository : BaseRepository<AfetacaoMensal,AfetacaoMensalId>,IAfetacaoMensalRepository
    {
        private readonly DDDSample1DbContext _context;
        
        public AfetacaoMensalRepository(DDDSample1DbContext context):base(context.AfetacaoMensais) {
            _context = context;
        }

        public new async Task<List<AfetacaoMensal>> GetAllAsync() {
            return await _context.AfetacaoMensais
                .Include(o => o.AfetacaoPerfil)
                .Include(o => o.CargaMensal)
                .ToListAsync();
        }
    }
}