using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Contratos
{
    public class DataInicioContrato : IValueObject
    {
        public DateTime InicioContrato {get; private set;}

        public DataInicioContrato(DateTime datainiciocontrato) {
            if(string.IsNullOrEmpty(datainiciocontrato.ToString()))
                throw new BusinessRuleValidationException("The date cannot be null or empty!");
            this.InicioContrato = datainiciocontrato;
        }
        
        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (DataInicioContrato)obj;
            return InicioContrato == other.InicioContrato;
        }

        public override int GetHashCode(){
            return InicioContrato.GetHashCode();
        }
    }
}