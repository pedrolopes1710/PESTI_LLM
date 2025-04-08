using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.Contratos;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;

namespace dddnetcore.Infraestructure.Contratos
{
    public class ContratoRepository : BaseRepository<Contrato,ContratoId>,IContratoRepository
    {
        public ContratoRepository(DDDSample1DbContext context):base(context.Contratos)
        {
           
        }
    }
}