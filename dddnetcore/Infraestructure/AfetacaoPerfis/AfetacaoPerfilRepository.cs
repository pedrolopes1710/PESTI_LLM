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
using dddnetcore.Domain.Pessoas;

namespace dddnetcore.Infraestructure.AfetacaoPerfis
{
    public class AfetacaoPerfilRepository : BaseRepository<AfetacaoPerfil,AfetacaoPerfilId>,IAfetacaoPerfilRepository
    {
        private readonly DDDSample1DbContext _context;
        
        public AfetacaoPerfilRepository(DDDSample1DbContext context):base(context.AfetacaoPerfis) {
            _context = context;
        }

        public new async Task<List<AfetacaoPerfil>> GetAllAsync() {
            return await _context.AfetacaoPerfis
                .Include(o => o.Pessoa)
                .Include(o => o.Perfil)
                .ToListAsync();
        }

        public new async Task<AfetacaoPerfil> GetByIdAsync(AfetacaoPerfilId id) {
            return await _context.AfetacaoPerfis
                .Include(o => o.Pessoa)
                .Include(o => o.Perfil)
                .FirstOrDefaultAsync(o => o.Id == id);
        }

        public async Task<List<AfetacaoPerfil>> GetByPessoaIdAsync(PessoaId pessoaId)
        {
            return await _context.AfetacaoPerfis
                .Where(a => a.Pessoa.Id == pessoaId)
                .Include(a => a.Pessoa)
                .Include(a => a.Perfil)
                .ToListAsync();
        } 
    }
}