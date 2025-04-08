using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Orcamentos
{
    public interface IOrcamentoRepository : IRepository<Orcamento, OrcamentoId>
    {
        public Task<List<Orcamento>> GetOrcamentosAsync(Guid? rubricaId = null);
    }
}