using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Perfis
{
    public class DescricaoPerfil : IValueObject
    {
        public string Valor { get; private set; }
        
        private DescricaoPerfil() { }

        public DescricaoPerfil(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new BusinessRuleValidationException("A descrição não pode ser vazia");

            Valor = descricao;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType()) return false;
            var other = (DescricaoPerfil)obj;
            return Valor == other.Valor;
        }

        public override int GetHashCode() => Valor.GetHashCode();
    }
}