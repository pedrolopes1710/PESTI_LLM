using DDDSample1.Domain.Shared;
using dddnetcore.Domain.Contratos;
using System.Threading.Tasks;

namespace dddnetcore.Domain.Pessoas
{
    public interface IPessoaRepository : IRepository<Pessoa, PessoaId>
    {
        public Task<Pessoa> GetByContratoIdAsync(ContratoId contratoId);
        
    }
}