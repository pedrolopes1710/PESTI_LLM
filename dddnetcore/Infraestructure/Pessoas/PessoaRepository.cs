using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Pessoas;
using dddnetcore.Domain.Contratos;
using dddnetcore.Domain.Projetos;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace dddnetcore.Infraestructure.Pessoas
{
    public class PessoaRepository : BaseRepository<Pessoa, PessoaId>, IPessoaRepository
    {
        private readonly DDDSample1DbContext _context;

        public PessoaRepository(DDDSample1DbContext context) : base(context.Pessoas)
        {
            _context = context;
        }

public async Task<List<Pessoa>> GetAllAsync()
{
    return await _context.Pessoas
        .Include(p => p.Projetos) // <- isto é essencial
        .ToListAsync();
}


        public new async Task<Pessoa> GetByIdAsync(PessoaId id)
        {
            return await _context.Pessoas
                .Include(p => p.Contrato)
                .Include(p => p.CargasMensais)
                .Include(p => p.Projetos) //talvez esta não faça sentido
                .FirstOrDefaultAsync(p => p.Id.Equals(id));
        }

        public async Task<Pessoa> GetByContratoIdAsync(ContratoId contratoId)
        {
            return await _context.Pessoas
                .FirstOrDefaultAsync(p => p.ContratoId == contratoId);
        }

public async Task<List<Projeto>> GetProjetosByPessoaIdAsync(PessoaId pessoaId)
{
    var pessoa = await _context.Pessoas
        .Include(p => p.Projetos)
        .FirstOrDefaultAsync(p => p.Id == pessoaId);

    return pessoa?.Projetos.ToList();
}   
    }
}