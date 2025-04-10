using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Indicadores
{
    public class NomeIndicador : IValueObject
    {
        public string Valor { get; private set; }

        public NomeIndicador(string valor)
        {
            if (string.IsNullOrWhiteSpace(valor))
                throw new BusinessRuleValidationException("O nome do indicador não pode ser vazio.");

            Valor = valor;
        }

        public override string ToString() => Valor;
    }
}