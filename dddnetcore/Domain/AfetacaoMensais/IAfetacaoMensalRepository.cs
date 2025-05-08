using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dddnetcore.Domain.CargasMensais;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.AfetacaoMensais
{
    public interface IAfetacaoMensalRepository : IRepository<AfetacaoMensal, AfetacaoMensalId>
    {
        Task<List<AfetacaoMensal>> GetByCargaMensalIdAsync(CargaMensalId cargaMensalId);
        public Task<AfetacaoMensal> UpdateAsync(AfetacaoMensal afetacaoMensal);
    }
}