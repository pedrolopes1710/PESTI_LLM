using DDDSample1.Domain.Shared;
using dddnetcore.Domain.Contratos;
using dddnetcore.Domain.Projetos;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace dddnetcore.Domain.Pessoas
{
    public interface IPessoaRepository : IRepository<Pessoa, PessoaId>
    {
        public Task<Pessoa> GetByContratoIdAsync(ContratoId contratoId);
        public Task<List<Projeto>> GetProjetosByPessoaIdAsync(PessoaId pessoaId);

        
    }
}