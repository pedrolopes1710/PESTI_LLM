using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Indicadores
{
    public class ValorMaximo : IValueObject
    {
        public double Valor { get; private set; }

        public ValorMaximo(double valor)
        {
            if (valor <= 0)
                throw new BusinessRuleValidationException("O valor máximo deve ser maior que zero.");

            Valor = valor;
        }
    }
}