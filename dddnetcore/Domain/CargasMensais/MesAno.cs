using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.CargasMensais
{
    public class MesAno : IValueObject
    {
        public DateTime Valor { get; private set; }

        public MesAno(DateTime valor)
        {
            if (valor == default)
                throw new BusinessRuleValidationException("Data de Mês/Ano inválida.");
            
            Valor = new DateTime(valor.Year, valor.Month, 1); // Normaliza para o 1º dia do mês
        }

        public override bool Equals(object obj) {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (MesAno)obj;
            return Valor == other.Valor;
        }

        public override int GetHashCode(){
            return Valor.GetHashCode();
        }
    }
}
