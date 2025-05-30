using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Orcamentos
{
    public interface IOrcamentoRepository : IRepository<Orcamento, OrcamentoId>
    {
        public Task<List<Orcamento>> GetOrcamentosAsync(Guid? rubricaId = null);
        public Task<List<Orcamento>> GetOrcamentosByAtividadeAsync(Guid? atividadeId = null);
        public Task<Orcamento> UpdateAsync(Orcamento orcamento);
    }
}