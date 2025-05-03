using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.AfetacaoMensais
{
    public interface IAfetacaoMensalRepository : IRepository<AfetacaoMensal, AfetacaoMensalId>
    {
        public Task<AfetacaoMensal> UpdateAsync(AfetacaoMensal afetacaoMensal);
    }
}