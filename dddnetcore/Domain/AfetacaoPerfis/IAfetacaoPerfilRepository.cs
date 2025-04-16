using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.AfetacaoPerfis
{
    public interface IAfetacaoPerfilRepository : IRepository<AfetacaoPerfil, AfetacaoPerfilId>
    {
        /*public Task<List<Orcamento>> GetOrcamentosAsync(Guid? rubricaId = null);
        public Task<Orcamento> UpdateAsync(Orcamento orcamento);*/
    }
}