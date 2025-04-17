using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Pessoas
{
    public interface IPessoaRepository : IRepository<Pessoa,PessoaId>
    {
        
    }
}