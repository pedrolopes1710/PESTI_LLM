
using System;
using dddnetcore.Domain.Projetos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace dddnetcore.Services
{
    public class ProjetoService
    {
        private readonly DDDSample1DbContext _context;

        public ProjetoService(DDDSample1DbContext context)
        {
            _context = context;
        }

        public async Task<List<Projeto>> GetAllAsync()
        {
            return await _context.Set<Projeto>().ToListAsync();
        }

        public async Task<Projeto> GetByIdAsync(Guid id)
        {
            return await _context.Set<Projeto>().FindAsync(new ProjetoId(id));
        }

        public async Task<Projeto> CreateAsync(string nome, string descricao)
        {
            var projeto = new Projeto(nome, descricao);
            _context.Set<Projeto>().Add(projeto);
            await _context.SaveChangesAsync();
            return projeto;
        }

       

        public async Task<bool> DeleteAsync(Guid id)
        {
            var projeto = await GetByIdAsync(id);
            if (projeto == null) return false;

            _context.Set<Projeto>().Remove(projeto);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
