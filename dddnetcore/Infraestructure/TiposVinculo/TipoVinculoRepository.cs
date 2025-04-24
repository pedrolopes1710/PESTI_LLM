using dddnetcore.Domain.TiposVinculo;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;

namespace dddnetcore.Infraestructure.TiposVinculo
{
    public class TipoVinculoRepository : BaseRepository<TipoVinculo,TipoVinculoId>,ITipoVinculoRepository
    {
        public TipoVinculoRepository(DDDSample1DbContext context):base(context.TiposVinculo)
        {
           
        }
    }
}