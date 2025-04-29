using System.Threading.Tasks;
using dddnetcore.Domain.Perfis;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;

namespace dddnetcore.Infrastructure.Perfis
{
    public class PerfilRepository : BaseRepository<Perfil, PerfilId>, IPerfilRepository
    {
        private readonly DDDSample1DbContext _context;
        public PerfilRepository(DDDSample1DbContext context) : base(context.Perfil)
        {
            _context = context;
        }

        public async Task<Perfil> UpdateAsync(Perfil perfil) {
            _context.Perfil.Update(perfil);

            await _context.SaveChangesAsync();

            return perfil;
        }
    }
}