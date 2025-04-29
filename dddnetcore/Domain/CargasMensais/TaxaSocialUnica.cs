using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.CargasMensais
{
    public class TaxaSocialUnica : IValueObject
    {
        public double Valor { get; private set; }

        public TaxaSocialUnica(double valor)
        {
            if (valor < 0)
                throw new BusinessRuleValidationException("Single Social Tax must be higher than 0.");
            
            Valor = valor;
        }

        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (TaxaSocialUnica)obj;
            return Valor == other.Valor;
        }

        public override int GetHashCode(){
            return Valor.GetHashCode();
        }
    }
}
