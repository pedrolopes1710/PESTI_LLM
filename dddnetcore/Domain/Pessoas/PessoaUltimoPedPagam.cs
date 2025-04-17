using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Pessoas
{
    public class PessoaUltimoPedPagam : IValueObject
    {
        public DateTime Value {get; private set;}

        public PessoaUltimoPedPagam(DateTime pessoaUltimoPedPagam) {
            if(string.IsNullOrEmpty(pessoaUltimoPedPagam.ToString()))
                throw new BusinessRuleValidationException("The date cannot be null or empty!");
            this.Value = pessoaUltimoPedPagam;
        }
        
        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (PessoaUltimoPedPagam)obj;
            return Value == other.Value;
        }

        public override int GetHashCode(){
            return Value.GetHashCode();
        }
    }
}