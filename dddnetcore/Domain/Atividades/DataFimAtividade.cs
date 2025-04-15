using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Atividades
{
    public class DataFimAtividade : IValueObject
    {
        public DateTime FimAtividade {get; private set;}

        public DataFimAtividade(DateTime datafimatividade) {
            if(string.IsNullOrEmpty(datafimatividade.ToString()))
                throw new BusinessRuleValidationException("The date cannot be null or empty!");
            this.FimAtividade = datafimatividade;
        }
        
        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (DataFimAtividade)obj;
            return FimAtividade == other.FimAtividade;
        }

        public override int GetHashCode(){
            return FimAtividade.GetHashCode();
        }
    }
}