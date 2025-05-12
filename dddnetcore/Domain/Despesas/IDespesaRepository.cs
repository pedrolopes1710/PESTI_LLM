using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Despesas
{
    public interface IDespesaRepository : IRepository<Despesa, DespesaId>
    {
        Task<Despesa> UpdateAsync(Despesa despesa);
    }
}