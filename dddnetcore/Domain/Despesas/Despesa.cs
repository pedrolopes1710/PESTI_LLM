using System;
using dddnetcore.Domain.CargasMensais;

using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Despesas
{
    public class Despesa : Entity<DespesaId>, IAggregateRoot
    {

        public DescricaoDespesa Descricao { get; private set; }
        public ValorDespesa Valor { get; private set; }
        public CargaMensal CargaMensal { get; private set; }
        public CargaMensalId CargaMensalId { get; private set; }
        public Automatico Automatico {get; private set;}


        private Despesa()
        {}

        public Despesa(DescricaoDespesa descricao, ValorDespesa valor, CargaMensal cargaMensal, Automatico automatico)
        {
            this.Id = new DespesaId(Guid.NewGuid());
            this.Descricao = descricao;
            this.Valor = valor;
            this.CargaMensal = cargaMensal;
            this.Automatico = automatico;
        }



    }
}
