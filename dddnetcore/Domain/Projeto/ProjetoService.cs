using System;
using dddnetcore.Domain.Projetos;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Pessoas;
using DDDSample1.Domain.Shared;
using DDDSample1.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace dddnetcore.Services
{
    public class ProjetoService
    {
        private readonly IPessoaRepository _pessoaRepository;
        private readonly DDDSample1DbContext _context;

        public ProjetoService(DDDSample1DbContext context, IPessoaRepository pessoaRepository)
        {
            _context = context;
            _pessoaRepository = pessoaRepository;
        }

        public async Task<List<ProjetoDTO>> GetAllAsync()
        {
            return await _context.Set<Projeto>()
                .Select(p => new ProjetoDTO
                {
                    Id = p.Id.AsGuid(),
                    Nome = p.NomeProjeto.Valor,
                    Descricao = p.DescricaoProjeto.Valor,
                    PessoaId = p.PessoaId.AsGuid()  
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
                Descricao = projeto.DescricaoProjeto.Valor,
                PessoaId = projeto.PessoaId.AsGuid() 
            };
        }

        public async Task<ProjetoDTO> CreateAsync(CreateProjetoDto dto)
        {
            var pessoa = await _pessoaRepository.GetByIdAsync(new PessoaId(dto.PessoaId));
            if (pessoa == null)
            {
                throw new BusinessRuleValidationException("Pessoa de ciência não encontrada.");
            }

            var projeto = new Projeto(dto.Nome, dto.Descricao, new PessoaId(dto.PessoaId));

            _context.Set<Projeto>().Add(projeto);
            await _context.SaveChangesAsync();

            return new ProjetoDTO
            {
                Id = projeto.Id.AsGuid(),
                Nome = projeto.NomeProjeto.Valor,
                Descricao = projeto.DescricaoProjeto.Valor,
                PessoaId = projeto.PessoaId.AsGuid() 
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
        
        public async Task<ProjetoDTO> UpdateAsync(Guid id, string nome, string descricao)
        {
            var projeto = await _context.Set<Projeto>().FindAsync(new ProjetoId(id));
            if (projeto == null) return null;

            if (!string.IsNullOrWhiteSpace(nome))
                projeto.AlterarNome(nome);

            if (!string.IsNullOrWhiteSpace(descricao))
                projeto.AlterarDescricao(descricao);

            _context.Set<Projeto>().Update(projeto);
            await _context.SaveChangesAsync();

            return new ProjetoDTO
            {
                Id = projeto.Id.AsGuid(),
                Nome = projeto.NomeProjeto.Valor,
                Descricao = projeto.DescricaoProjeto.Valor,
                PessoaId = projeto.PessoaId.AsGuid() 
            };
        }
    }
}
