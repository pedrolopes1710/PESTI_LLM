using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using dddnetcore.Domain.Pessoas;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.AfetacaoPerfis
{
    public interface IAfetacaoPerfilRepository : IRepository<AfetacaoPerfil, AfetacaoPerfilId>
    {
        public Task<List<AfetacaoPerfil>> GetByPessoaIdAsync(PessoaId pessoaId);
    }
}