using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dddnetcore.Domain.Despesas
{
    public class EditingDespesaDto
    {
        public string Descricao { get; set; }
        public double? Valor { get; set; }
    }
}