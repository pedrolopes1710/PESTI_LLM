using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.CargasMensais;
using DDDSample1.Infrastructure;
using DDDSample1.Infrastructure.Shared;

namespace dddnetcore.Infraestructure.CargasMensais
{
    public class CargaMensalRepository : BaseRepository<CargaMensal,CargaMensalId>,ICargaMensalRepository
    {
        private readonly DDDSample1DbContext _context;

        public CargaMensalRepository(DDDSample1DbContext context):base(context.CargasMensais)
        {
            _context = context;
           
        }
    }
}