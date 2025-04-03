using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Orcamentos
{
    public class GastoPlaneado : IValueObject
    {
        public double Quantidade {get; private set;}

        public GastoPlaneado(double quantidade) {
            if (quantidade < 0)
                throw new BusinessRuleValidationException("Planned spending cannot be negative!");
            this.Quantidade = quantidade;
        }

        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (GastoPlaneado)obj;
            return Quantidade == other.Quantidade;
        }

        public override int GetHashCode(){
            return Quantidade.GetHashCode();
        }
    }
}