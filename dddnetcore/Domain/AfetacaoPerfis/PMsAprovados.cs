using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.AfetacaoPerfis
{
    public class PMsAprovados : IValueObject
    {
        public double Quantidade {get; private set;}

        public PMsAprovados(double quantidade) {
            if (quantidade < 0)
                throw new BusinessRuleValidationException("Approved PMs cannot be negative!");
            this.Quantidade = quantidade;
        }

        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (PMsAprovados)obj;
            return Quantidade == other.Quantidade;
        }

        public override int GetHashCode(){
            return Quantidade.GetHashCode();
        }        
    }
}