using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.ConsumoPerfis
{
    public class PMsConsumidos : IValueObject
    {
        public int PMs {get; private set;}

        public PMsConsumidos(int pms) {
            if(String.IsNullOrEmpty(pms.ToString()))
                throw new BusinessRuleValidationException("The pms cannot be null or empty!");
            this.PMs = pms;
        }
        
        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (PMsConsumidos)obj;
            return PMs == other.PMs;
        }

        public override int GetHashCode(){
            return PMs.GetHashCode();
        }
    }
}