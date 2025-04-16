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

        /*public async Task<List<Orcamento>> GetOrcamentosAsync(Guid? rubricaId = null)
        {
            if (rubricaId == null) return await GetAllAsync();

            var query = _context.Orcamentos.AsQueryable();

            query = query.Where(orcamento => orcamento.Rubrica.Id.Equals(new RubricaId(rubricaId.Value)));

            return await query.ToListAsync();
        }

        public async Task<Orcamento> UpdateAsync(Orcamento orcamento) {
            _context.Orcamentos.Update(orcamento);

            await _context.SaveChangesAsync();

            return orcamento;
        }*/
    }
}