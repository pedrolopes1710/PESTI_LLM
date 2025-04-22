using dddnetcore.Domain.Projetos;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;

namespace dddnetcore.Infrastructure.Projetos
{
    public class ProjetoRepository : BaseRepository<Projeto, ProjetoId>, IProjetoRepository
    {
        public ProjetoRepository(DDDSample1DbContext context) : base(context.Projetos)
        {
        }
    }
}