using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Tarefas
{
    public class DescricaoTarefa : IValueObject
    {
        public String Descricao {get; private set;}

        public DescricaoTarefa(String descricao) {
            if(string.IsNullOrEmpty(descricao))
                throw new BusinessRuleValidationException("The description cannot be null or empty!");
            this.Descricao = descricao;
        }
        
        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (DescricaoTarefa)obj;
            return Descricao == other.Descricao;
        }

        public override int GetHashCode(){
            return Descricao.GetHashCode();
        }
    }
}