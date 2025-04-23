using dddnetcore.Domain.TiposEntregavel;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;

namespace dddnetcore.Infraestructure.TiposEntregavel
{
    public class TipoEntregavelRepository : BaseRepository<TipoEntregavel,TipoEntregavelId>,ITipoEntregavelRepository
    {
        public TipoEntregavelRepository(DDDSample1DbContext context):base(context.TiposEntregavel)
        {
           
        }
    }
}