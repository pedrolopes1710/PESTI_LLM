using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.TiposEntregavel
{
    public interface ITipoEntregavelRepository : IRepository<TipoEntregavel, TipoEntregavelId>
    {
       
    }
}