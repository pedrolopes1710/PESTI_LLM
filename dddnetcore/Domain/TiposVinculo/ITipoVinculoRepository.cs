using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.TiposVinculo
{
    public interface ITipoVinculoRepository : IRepository<TipoVinculo, TipoVinculoId>
    {
       
    }
}