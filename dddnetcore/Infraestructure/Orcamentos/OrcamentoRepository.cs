using dddnetcore.Domain.Orcamentos;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;

namespace dddnetcore.Infraestructure.Orcamentos
{
    public class OrcamentoRepository : BaseRepository<Orcamento,OrcamentoId>,IOrcamentoRepository
    {
        public OrcamentoRepository(DDDSample1DbContext context):base(context.Orcamentos) {
            
        }
    }
}