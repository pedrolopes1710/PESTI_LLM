using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dddnetcore.Domain.Orcamentos
{
    public class EditingOrcamentoDto
    {
        public double? GastoExecutado {get;set;}
        public double? GastoPlaneado {get;set;} 
        public Guid? RubricaId {get;set;}
    }
}