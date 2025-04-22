using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.CargasMensais
{
    public class SalarioBase : IValueObject
    {
        public double Valor { get; private set; }

        public SalarioBase(double valor)
        {
            if (valor <= 0)
                throw new BusinessRuleValidationException("Salário base deve ser maior que zero.");
            
            Valor = valor;
        }

        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (SalarioBase)obj;
            return Valor == other.Valor;
        }

        public override int GetHashCode(){
            return Valor.GetHashCode();
        }
    }
}
