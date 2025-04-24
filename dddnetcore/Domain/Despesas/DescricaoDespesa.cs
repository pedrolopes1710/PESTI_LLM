using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Despesas
{
    public class DescricaoDespesa : IValueObject
    {
        public String Descricao { get; private set; }

        public DescricaoDespesa(String descricao)
        {
            if (string.IsNullOrEmpty(descricao))
                throw new BusinessRuleValidationException("The expense description cannot be null or empty!");
            this.Descricao = descricao;
        }

        public override bool Equals(object obj)
        {
            if (obj == null || GetType() != obj.GetType())
                return false;

            var other = (DescricaoDespesa)obj;
            return Descricao == other.Descricao;
        }

        public override int GetHashCode()
        {
            return Descricao.GetHashCode();
        }
    }
}