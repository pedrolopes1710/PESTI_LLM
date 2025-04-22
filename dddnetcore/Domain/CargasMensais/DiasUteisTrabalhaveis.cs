using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.CargasMensais
{
    public class DiasUteisTrabalhaveis : IValueObject
    {
        public double Valor { get; private set; }

        public DiasUteisTrabalhaveis(double valor)
        {
            if (valor < 0 || valor > 31)
                throw new BusinessRuleValidationException("Número de dias úteis inválido.");
            
            Valor = valor;
        }

        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (DiasUteisTrabalhaveis)obj;
            return Valor == other.Valor;
        }

        public override int GetHashCode(){
            return Valor.GetHashCode();
        }
    }
}
