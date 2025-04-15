using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Atividades
{
    public class DataInicioAtividade : IValueObject
    {
        public DateTime InicioAtividade {get; private set;}

        public DataInicioAtividade(DateTime datainicioatividade) {
            if(string.IsNullOrEmpty(datainicioatividade.ToString()))
                throw new BusinessRuleValidationException("The date cannot be null or empty!");
            this.InicioAtividade = datainicioatividade;
        }
        
        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (DataInicioAtividade)obj;
            return InicioAtividade == other.InicioAtividade;
        }

        public override int GetHashCode(){
            return InicioAtividade.GetHashCode();
        }
    }
}