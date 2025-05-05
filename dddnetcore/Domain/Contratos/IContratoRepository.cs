using DDDSample1.Domain.Shared;
using System.Threading.Tasks;
using dddnetcore.Domain.Pessoas;

namespace dddnetcore.Domain.Contratos
{
    public interface IContratoRepository : IRepository<Contrato,ContratoId>
    {

    Task<Contrato> GetByPessoaIdAsync(PessoaId pessoaId);

        
    }
}