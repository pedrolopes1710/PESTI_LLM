using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.CargasMensais
{
    public class JornadaDiaria : IValueObject
    {
        public double Valor { get; private set; }

        public JornadaDiaria(double valor)
        {
            if (valor <= 0)
                throw new BusinessRuleValidationException("A jornada diária deve ser maior que zero.");
            
            Valor = valor;
        }

        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (JornadaDiaria)obj;
            return Valor == other.Valor;
        }

        public override int GetHashCode(){
            return Valor.GetHashCode();
        }
    }
}
