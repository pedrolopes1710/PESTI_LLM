using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Pessoas
{
    public class PessoaCienciaId : IValueObject
    {
        public string Value {get; private set;}

        public PessoaCienciaId(String cienciaId) {
            if(string.IsNullOrEmpty(cienciaId))
                throw new BusinessRuleValidationException("Ciencia ID cannot be null or empty!");
            this.Value = Value;
        }
        
        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (NomePessoa)obj;
            return Value == other.Value;
        }

        public override int GetHashCode(){
            return Value.GetHashCode();
        }
    }
}