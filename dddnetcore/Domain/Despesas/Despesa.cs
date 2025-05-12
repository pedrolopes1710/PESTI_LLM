using System;
using dddnetcore.Domain.CargasMensais;
using dddnetcore.Domain.Orcamentos;
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
        public OrcamentoId OrcamentoId { get; private set; }


        private Despesa()
        {}

        public Despesa(DescricaoDespesa descricao, ValorDespesa valor, CargaMensal cargaMensal, Automatico automatico, OrcamentoId orcamentoId)
        {
            this.Id = new DespesaId(Guid.NewGuid());
            this.Descricao = descricao;
            this.Valor = valor;
            this.CargaMensal = cargaMensal;
            this.Automatico = automatico;
            this.OrcamentoId = orcamentoId;
        }

        public void EditValor(ValorDespesa valor)
        {
            if (Automatico.Auto) throw new BusinessRuleValidationException("Automatic expenses cannot be edited!");
            if (valor == null) throw new ArgumentNullException("Missing value for Expense edition!");
            this.Valor = valor;
        }

        public void EditDescricao(DescricaoDespesa descricao)
        {
            if (Automatico.Auto) throw new BusinessRuleValidationException("Automatic expenses cannot be edited!");
            if (descricao == null) throw new ArgumentNullException("Missing name for Expense edition!");
            this.Descricao = descricao;
        }

    }
}
