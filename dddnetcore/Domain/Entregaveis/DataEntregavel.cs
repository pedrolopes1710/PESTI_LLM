using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Entregaveis
{
    public class DataEntregavel : IValueObject
    {
        public DateTime Data {get; private set;}

        public DataEntregavel(DateTime data) {
            if (data == DateTime.MinValue)
                throw new BusinessRuleValidationException("The date cannot be empty or uninitialized.");
    
            this.Data = data;
        }
        
        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (DataEntregavel)obj;
            return Data == other.Data;
        }

        public override int GetHashCode(){
            return Data.GetHashCode();
        }
    }
}