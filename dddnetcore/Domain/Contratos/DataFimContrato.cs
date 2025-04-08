using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Contratos
{
    public class DataFimContrato : IValueObject
    {
        public DateTime FimContrato {get; private set;}

        public DataFimContrato(DateTime datafimcontrato) {
            if(string.IsNullOrEmpty(datafimcontrato.ToString()))
                throw new BusinessRuleValidationException("The date cannot be null or empty!");
            this.FimContrato = datafimcontrato;
        }
        
        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (DataFimContrato)obj;
            return FimContrato == other.FimContrato;
        }

        public override int GetHashCode(){
            return FimContrato.GetHashCode();
        }
    }
}