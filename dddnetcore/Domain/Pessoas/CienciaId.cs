using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Pessoas
{
    public class CienciaId : IValueObject
    {
        public String Ciencia {get; private set;}

        public CienciaId(String cienciaId) {
            if(string.IsNullOrEmpty(cienciaId))
                throw new BusinessRuleValidationException("The name cannot be null or empty!");
            this.Ciencia = cienciaId;
        }
        
        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (CienciaId)obj;
            return Ciencia == other.Ciencia;
        }

        public override int GetHashCode(){
            return Ciencia.GetHashCode();
        }
    }
}