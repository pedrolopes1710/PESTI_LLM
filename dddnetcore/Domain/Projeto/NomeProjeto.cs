using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Projetos
{
    public class NomeProjeto : IValueObject
    {
        public string Valor { get; private set; }
        
        public NomeProjeto() { }

        public NomeProjeto(string nome)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new BusinessRuleValidationException("O nome do projeto não pode ser vazio");

            Valor = nome;
        }

        public override bool Equals(object obj)
        {
            return obj is NomeProjeto other && Valor == other.Valor;
        }

        public override int GetHashCode() => Valor.GetHashCode();
    }
}