using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Indicadores
{
    public class ValorAtual : IValueObject
    {
        public double Valor { get; private set; }

        public ValorAtual(double valor)
        {
            if (valor < 0)
                throw new BusinessRuleValidationException("O valor atual não deve ser negativo.");

            Valor = valor;
        }
    }
}