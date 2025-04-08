using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.TiposVinculo
{
    public class NomeVinculo : IValueObject
    {
        public String Nome {get; private set;}

        public NomeVinculo(String nome) {
            if(string.IsNullOrEmpty(nome))
                throw new BusinessRuleValidationException("The name cannot be null or empty!");
            this.Nome = nome;
        }
        
        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (NomeVinculo)obj;
            return Nome == other.Nome;
        }

        public override int GetHashCode(){
            return Nome.GetHashCode();
        }
    }
}