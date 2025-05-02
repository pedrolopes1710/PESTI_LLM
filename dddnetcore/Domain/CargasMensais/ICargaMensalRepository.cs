using System;
using System.Threading.Tasks;
using dddnetcore.Domain.Pessoas;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.CargasMensais
{
    public interface ICargaMensalRepository : IRepository<CargaMensal,CargaMensalId>
    {
        Task<CargaMensal> GetByMesAnoAndPessoaAsync(MesAno mesAno, PessoaId pessoaId);
    }
}