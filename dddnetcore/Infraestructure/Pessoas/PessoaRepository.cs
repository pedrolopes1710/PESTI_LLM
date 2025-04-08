using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Pessoas;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;

namespace dddnetcore.Infraestructure.Pessoas
{
    public class PessoaRepository : BaseRepository<Pessoa,PessoaId>,IPessoaRepository
    {
        public PessoaRepository(DDDSample1DbContext context):base(context.Pessoas)
        {
           
        }
    }
}