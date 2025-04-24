using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Despesas
{
    public class ValorDespesa : IValueObject
    {
        public double Valor { get; private set; }

        public ValorDespesa(double valor)
        {
            if (valor < 0)
                throw new BusinessRuleValidationException("The expense value cannot be negative!");
            this.Valor = valor;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (ValorDespesa)obj;
            return Valor == other.Valor;
        }

        public override int GetHashCode()
        {
            return Valor.GetHashCode();
        }
    }
}