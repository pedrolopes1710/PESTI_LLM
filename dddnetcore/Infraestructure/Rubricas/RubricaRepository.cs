using dddnetcore.Domain.Rubricas;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;

namespace dddnetcore.Infraestructure.Rubricas
{
    public class RubricaRepository : BaseRepository<Rubrica,RubricaId>,IRubricaRepository
    {
        public RubricaRepository(DDDSample1DbContext context):base(context.Rubricas)
        {
           
        }
    }
}