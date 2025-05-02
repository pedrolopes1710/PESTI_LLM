using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Pessoas;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;
using Microsoft.EntityFrameworkCore;

namespace dddnetcore.Infraestructure.Orcamentos
{
    public class PessoaRepository : BaseRepository<Pessoa,PessoaId>,IPessoaRepository
    {
        private readonly DDDSample1DbContext _context;
        
        public PessoaRepository(DDDSample1DbContext context):base(context.Pessoas) {
            _context = context;
        }

    }
}