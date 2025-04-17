using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Pessoas
{
    public class NomePessoa : IValueObject
    {
        public string Value {get; private set;}

        public NomePessoa(String nome) {
            if(string.IsNullOrEmpty(nome))
                throw new BusinessRuleValidationException("The name cannot be null or empty!");
            this.Value = nome;
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