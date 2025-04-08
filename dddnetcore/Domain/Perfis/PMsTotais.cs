using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Perfis
{
    public class PMsTotais : IValueObject
    {
        public int PMs {get; private set;}

        public PMsTotais(int pms) {
            if(String.IsNullOrEmpty(pms.ToString()))
                throw new BusinessRuleValidationException("The pms cannot be null or empty!");
            this.PMs = pms;
        }
        
        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (PMsTotais)obj;
            return PMs == other.PMs;
        }

        public override int GetHashCode(){
            return PMs.GetHashCode();
        }
    }
}