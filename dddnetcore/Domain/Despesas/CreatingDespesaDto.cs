using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dddnetcore.Domain.Despesas
{
    public class CreatingDespesaDto
    {
        public string Descricao { get; set; }
        public double Valor { get; set; }
        public Guid? CargaMensalId { get; set; } // Nullable Guid to allow null values for CargaMensalId
    }
}