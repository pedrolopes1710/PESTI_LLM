using dddnetcore.Domain.Indicadores;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;

namespace dddnetcore.Infrastructure.Indicadores
{
    public class IndicadorRepository : BaseRepository<Indicador, IndicadorId>, IIndicadorRepository
    {
        public IndicadorRepository(DDDSample1DbContext context) : base(context.Indicadores)
        {
        }
    }
}