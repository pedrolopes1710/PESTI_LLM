using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Orcamentos
{
    public class GastoExecutado : IValueObject
    {
        public double Quantidade {get; private set;}

        public GastoExecutado(double quantidade) {
            if (quantidade < 0)
                throw new BusinessRuleValidationException("Executive spending cannot be negative!");
            this.Quantidade = quantidade;
        }

        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (GastoExecutado)obj;
            return Quantidade == other.Quantidade;
        }

        public override int GetHashCode(){
            return Quantidade.GetHashCode();
        }
    }
}