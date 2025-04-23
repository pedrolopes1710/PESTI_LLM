using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Entregaveis
{
    public interface IEntregavelRepository : IRepository<Entregavel, EntregavelId>
    {
       
    }
}