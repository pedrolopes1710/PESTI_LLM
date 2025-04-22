using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Projetos
{
    public class DescricaoProjeto : IValueObject
    {
        public string Valor { get; private set; }

        public DescricaoProjeto() { }

        public DescricaoProjeto(string descricao)
        {
            if (string.IsNullOrWhiteSpace(descricao))
                throw new BusinessRuleValidationException("A descrição do projeto não pode ser vazia");

            Valor = descricao;
        }

        public override bool Equals(object obj)
        {
            return obj is DescricaoProjeto other && Valor == other.Valor;
        }

        public override int GetHashCode() => Valor.GetHashCode();
    }
}