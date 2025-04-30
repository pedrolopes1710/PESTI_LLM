using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Despesas
{
    public class Automatico : IValueObject
    {
        public Boolean Auto { get; private set; }
        
        public Automatico(Boolean auto)
        {
            this.Auto = auto;
        }
    }
}