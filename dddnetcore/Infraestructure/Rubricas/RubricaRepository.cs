using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Rubricas;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace dddnetcore.Infraestructure.Rubricas
{
    public class RubricaRepository : BaseRepository<Rubrica,RubricaId>,IRubricaRepository
    {
        private readonly DDDSample1DbContext _context;

        public RubricaRepository(DDDSample1DbContext context):base(context.Rubricas) {}
        
        public async Task<Rubrica> GetSalarial()
        {
            var query = _context.Rubricas.AsQueryable();
            query = query.Where(rubrica => rubrica.Nome.Equals(new NomeRubrica("Salarial")));
            return await query.FirstOrDefaultAsync();
        }
    }
}