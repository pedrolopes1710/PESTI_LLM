using dddnetcore.Domain.Perfis;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;

namespace dddnetcore.Infrastructure.Perfis
{
    public class PerfilRepository : BaseRepository<Perfil, PerfilId>, IPerfilRepository
    {
        public PerfilRepository(DDDSample1DbContext context) : base(context.Perfil)
        {
        }
    }
}