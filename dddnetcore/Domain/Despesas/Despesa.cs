using System;
using dddnetcore.Domain.CargasMensais;

#nullable enable
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Despesas
{
    public class Despesa : Entity<DespesaId>, IAggregateRoot
    {

        public DescricaoDespesa Descricao { get; private set; }
        public ValorDespesa Valor { get; private set; }
        public CargaMensal? CargaMensal { get; private set; }

        private Despesa()
        {
            this.Descricao = default!;
            this.Valor = default!;
        }

        public Despesa(DescricaoDespesa descricao, ValorDespesa valor, CargaMensal? cargaMensal = null)
        {
            this.Id = new DespesaId(Guid.NewGuid());
            this.Descricao = descricao;
            this.Valor = valor;
            this.CargaMensal = cargaMensal;
        }

    }
}
