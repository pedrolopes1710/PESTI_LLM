using System;
using DDDSample1.Domain.Shared;
using System.Text.RegularExpressions;


namespace dddnetcore.Domain.Pessoas
{
    public class PessoaCienciaId : IValueObject
    {
        private static readonly Regex _regex = new Regex(@"^[A-Za-z0-9]{4}-[A-Za-z0-9]{4}-[A-Za-z0-9]{4}$");

        public string Value { get; private set;}

        public PessoaCienciaId(string value)
        {
            if (!_regex.IsMatch(value))
            throw new ArgumentException("Invalid Science ID Identifier.", nameof(value));

            Value = value;
        }
        
        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (PessoaCienciaId)obj;
            return Value == other.Value;
        }

        public override int GetHashCode(){
            return Value.GetHashCode();
        }
    }
}