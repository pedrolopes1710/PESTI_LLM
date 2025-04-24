using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Despesas;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;

namespace dddnetcore.Infraestructure.Despesas
{
    public class DespesaRepository : BaseRepository<Despesa, DespesaId>, IDespesaRepository
    {
        public DespesaRepository(DDDSample1DbContext context) : base(context.Despesas)
        {
            
        }
    }
}