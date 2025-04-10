using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.TiposEntregavel
{
    public class NomeTipoEntregavel : IValueObject
    {
        public String Nome {get; private set;}

        public NomeTipoEntregavel(String nome) {
            if(string.IsNullOrEmpty(nome))
                throw new BusinessRuleValidationException("The name cannot be null or empty!");
            this.Nome = nome;
        }
        
        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (NomeTipoEntregavel)obj;
            return Nome == other.Nome;
        }

        public override int GetHashCode(){
            return Nome.GetHashCode();
        }
    }
}