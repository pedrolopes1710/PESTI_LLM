using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Tarefas
{
    public class NomeTarefa : IValueObject
    {
        public String Nome {get; private set;}

        public NomeTarefa(String nome) {
            if(string.IsNullOrEmpty(nome))
                throw new BusinessRuleValidationException("The name cannot be null or empty!");
            this.Nome = nome;
        }
        
        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (NomeTarefa)obj;
            return Nome == other.Nome;
        }

        public override int GetHashCode(){
            return Nome.GetHashCode();
        }
    }
}