using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Pessoas
{
    public class EmailPessoa : IValueObject
    {
        public String Email {get; private set;}

        public EmailPessoa(String email) {
            if(string.IsNullOrEmpty(email))
                throw new BusinessRuleValidationException("The name cannot be null or empty!");
            this.Email = email;
        }
        
        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (EmailPessoa)obj;
            return Email == other.Email;
        }

        public override int GetHashCode(){
            return Email.GetHashCode();
        }
    }
}