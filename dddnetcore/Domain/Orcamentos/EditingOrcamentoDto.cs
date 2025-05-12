using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dddnetcore.Domain.Orcamentos
{
    public class EditingOrcamentoDto
    {
        public double? GastoPlaneado {get;set;} 
        public Guid? RubricaId {get;set;}
        public Guid? DespesaId {get;set;}
    }
}