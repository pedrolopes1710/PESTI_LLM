using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Entregaveis
{
    public class DescricaoEntregavel : IValueObject
    {
        public String Descricao {get; private set;}

        public DescricaoEntregavel(String nome) {
            if(string.IsNullOrEmpty(nome))
                throw new BusinessRuleValidationException("The name cannot be null or empty!");
            this.Descricao = nome;
        }
        
        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (DescricaoEntregavel)obj;
            return Descricao == other.Descricao;
        }

        public override int GetHashCode(){
            return Descricao.GetHashCode();
        }
    }
}