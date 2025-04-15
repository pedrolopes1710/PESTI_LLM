using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Atividades
{
    public interface IAtividadeRepository : IRepository<Atividade, AtividadeId>
    {
        /*public Task<List<Orcamento>> GetOrcamentosAsync(Guid? rubricaId = null);
        public Task<Orcamento> UpdateAsync(Orcamento orcamento);*/
    }
}