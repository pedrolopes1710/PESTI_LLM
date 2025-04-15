using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Contratos
{
    public class SalarioMensalContrato : IValueObject
    {
        public double Valor { get; private set; }

        public SalarioMensalContrato(double valor)
        {
            if (valor <= 0)
            {
                throw new BusinessRuleValidationException("O salário mensal deve ser um valor positivo.");
            }

            this.Valor = valor;
        }

        public override string ToString()
        {
            return Valor.ToString("F2"); // Ex: 1500.00
        }
    }
}
