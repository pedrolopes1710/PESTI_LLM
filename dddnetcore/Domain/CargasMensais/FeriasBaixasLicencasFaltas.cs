using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.CargasMensais
{
    public class FeriasBaixasLicencasFaltas : IValueObject
    {
        public double Dias { get; private set; }

        public FeriasBaixasLicencasFaltas(double valor)
        {
            if (valor < 0)
                throw new BusinessRuleValidationException("O número de ausências não pode ser negativo.");
            
            Dias = valor;
        }

        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (FeriasBaixasLicencasFaltas)obj;
            return Dias == other.Dias;
        }

        public override int GetHashCode(){
            return Dias.GetHashCode();
        }
    }
}
