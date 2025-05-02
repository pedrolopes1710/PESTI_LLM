
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

        public async Task<List<ProjetoDTO>> GetAllAsync()
        {
            return await _context.Set<Projeto>()
                .Select(p => new ProjetoDTO
                {
                    Id = p.Id.AsGuid(),
                    Nome = p.NomeProjeto.Valor,
                    Descricao = p.DescricaoProjeto.Valor
                })
                .ToListAsync();
        }

        public async Task<ProjetoDTO> GetByIdAsync(Guid id)
        {
            var projeto = await _context.Set<Projeto>().FindAsync(new ProjetoId(id));
            if (projeto == null) return null;

            return new ProjetoDTO
            {
                Id = projeto.Id.AsGuid(),
                Nome = projeto.NomeProjeto.Valor,
                Descricao = projeto.DescricaoProjeto.Valor
            };
        }

        public async Task<ProjetoDTO> CreateAsync(string nome, string descricao)
        {
            var projeto = new Projeto(nome, descricao);
            _context.Set<Projeto>().Add(projeto);
            await _context.SaveChangesAsync();

            return new ProjetoDTO
            {
                Id = projeto.Id.AsGuid(),
                Nome = projeto.NomeProjeto.Valor,
                Descricao = projeto.DescricaoProjeto.Valor
            };
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            var projeto = await _context.Set<Projeto>().FindAsync(new ProjetoId(id));
            if (projeto == null) return false;

            _context.Set<Projeto>().Remove(projeto);
            await _context.SaveChangesAsync();
            return true;
        }
    }

    }

