using System.Threading.Tasks;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Rubricas
{
    public interface IRubricaRepository : IRepository<Rubrica,RubricaId>
    {
        public Task<Rubrica> GetSalarial();
    }
}