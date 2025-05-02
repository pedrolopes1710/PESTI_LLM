using DDDSample1.Domain.Shared;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace dddnetcore.Domain.Projetos
{
    public interface IProjetoRepository : IRepository<Projeto, ProjetoId>
    {

        public Task<List<Projeto>> GetByIdsAsync(IEnumerable<ProjetoId> ids);
    }
}