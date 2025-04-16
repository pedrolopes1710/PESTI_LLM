using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.AfetacaoPerfis
{
    public class DuracaoMes : IValueObject
    {
        public int Quantidade {get; private set;}

        public DuracaoMes(int quantidade) {
            if (quantidade < 0)
                throw new BusinessRuleValidationException("Durantion(months) cannot be negative!");
            this.Quantidade = quantidade;
        }

        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (DuracaoMes)obj;
            return Quantidade == other.Quantidade;
        }

        public override int GetHashCode(){
            return Quantidade.GetHashCode();
        }        
    }
}