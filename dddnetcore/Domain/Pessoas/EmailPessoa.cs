using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Pessoas
{
    public class EmailPessoa : IValueObject
    {
        public string Value {get; private set;}

        public EmailPessoa(string email) {
            if(string.IsNullOrEmpty(email))
                throw new BusinessRuleValidationException("The email cannot be null or empty!");
            this.Value = email;
        }

        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (EmailPessoa)obj;
            return Value == other.Value;
        }

        public override int GetHashCode(){
            return Value.GetHashCode();
        }        
    }
}