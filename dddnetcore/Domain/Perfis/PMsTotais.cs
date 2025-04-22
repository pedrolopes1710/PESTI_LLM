using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Perfis
{
    public class PMsTotais : IValueObject
    {
        public int Valor { get; private set; }
        
        private PMsTotais() { }

        public PMsTotais(int pms)
        {
            if (pms < 0)
                throw new BusinessRuleValidationException("Os PMs não podem ser negativos!");

            Valor = pms;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (PMsTotais)obj;
            return Valor == other.Valor;
        }

        public override int GetHashCode() => Valor.GetHashCode();
    }
}