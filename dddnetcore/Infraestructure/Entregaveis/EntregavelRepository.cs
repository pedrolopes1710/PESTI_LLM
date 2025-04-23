using dddnetcore.Domain.Entregaveis;
using dddnetcore.Domain.Entregavel;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;

namespace dddnetcore.Infraestructure.TiposEntregavel
{
    public class EntregavelRepository : BaseRepository<Entregavel,EntregavelId>,IEntregavelRepository
    {
        public EntregavelRepository(DDDSample1DbContext context):base(context.Entregaveis)
        {
           
        }
    }
}