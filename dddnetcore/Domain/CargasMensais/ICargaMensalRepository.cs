using System;
using System.Threading.Tasks;
using dddnetcore.Domain.Pessoas;
using DDDSample1.Domain.Shared;
using System.Collections.Generic;


namespace dddnetcore.Domain.CargasMensais
{
    public interface ICargaMensalRepository : IRepository<CargaMensal,CargaMensalId>
    {
        Task<CargaMensal> GetByMesAnoAndPessoaAsync(MesAno mesAno, PessoaId pessoaId);
        Task<List<CargaMensal>> GetByPessoaIdAsync(PessoaId pessoaId);

    }
}