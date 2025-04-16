using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.AfetacaoMensais
{
    public class PMs : IValueObject
    {
        public double Quantidade {get; private set;}

        public PMs(double quantidade) {
            if (quantidade < 0)
                throw new BusinessRuleValidationException("PMs cannot be negative!");
            this.Quantidade = quantidade;
        }

        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (PMs)obj;
            return Quantidade == other.Quantidade;
        }

        public override int GetHashCode(){
            return Quantidade.GetHashCode();
        }        
    }
}