using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Perfis
{
    public class DescricaoPerfil : IValueObject
    {
        public String Descricao {get; private set;}

        public DescricaoPerfil(String nome) {
            if(string.IsNullOrEmpty(nome))
                throw new BusinessRuleValidationException("The name cannot be null or empty!");
            this.Descricao = nome;
        }
        
        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (DescricaoPerfil)obj;
            return Descricao == other.Descricao;
        }

        public override int GetHashCode(){
            return Descricao.GetHashCode();
        }
    }
}