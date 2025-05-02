using System.Threading.Tasks;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Perfis
{
    public interface IPerfilRepository : IRepository<Perfil, PerfilId>
    {
        public Task<Perfil> UpdateAsync(Perfil perfil);
    }
}