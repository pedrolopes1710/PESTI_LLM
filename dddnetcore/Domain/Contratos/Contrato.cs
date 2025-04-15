using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Contratos
{
    public class Contrato : Entity<ContratoId>, IAggregateRoot
    {
        public TipoContrato Tipo { get; private set; }
        public SalarioMensalContrato Salario { get; private set; }
        public DataInicioContrato DataInicio { get; private set; }
        public DataFimContrato DataFim { get; private set; }
        public bool Ativo { get; private set; }

        private Contrato() { }

        public Contrato(
            TipoContrato tipo,
            SalarioMensalContrato salario,
            DataInicioContrato dataInicio,
            DataFimContrato dataFim)
        {
            if (dataFim != null && dataFim.FimContrato < dataInicio.InicioContrato)
                throw new BusinessRuleValidationException("A data de fim não pode ser anterior à data de início.");

            this.Id = new ContratoId(Guid.NewGuid());
            this.Tipo = tipo;
            this.Salario = salario ?? throw new ArgumentNullException(nameof(salario));
            this.DataInicio = dataInicio ?? throw new ArgumentNullException(nameof(dataInicio));
            this.DataFim = dataFim;
            this.Ativo = true;
        }

        public void AlterarTipo(TipoContrato novoTipo)
        {
            this.Tipo = novoTipo;
        }

        public void AlterarSalario(SalarioMensalContrato novoSalario)
        {
            if (novoSalario == null) throw new ArgumentNullException(nameof(novoSalario));
            this.Salario = novoSalario;
        }

        public void AlterarDataInicio(DataInicioContrato novaDataInicio)
        {
            if (novaDataInicio == null) throw new ArgumentNullException(nameof(novaDataInicio));
            if (DataFim != null && DataFim.FimContrato < novaDataInicio.InicioContrato)
                throw new BusinessRuleValidationException("A data de início não pode ser posterior à data de fim.");
            this.DataInicio = novaDataInicio;
        }

        public void AlterarDataFim(DataFimContrato novaDataFim)
        {
            if (novaDataFim != null && novaDataFim.FimContrato < DataInicio.InicioContrato)
                throw new BusinessRuleValidationException("A data de fim não pode ser anterior à data de início.");
            this.DataFim = novaDataFim;
        }

        public void Desativar()
        {
            this.Ativo = false;
        }

        public void Ativar()
        {
            this.Ativo = true;
        }

        public override string ToString()
        {
            return $"Contrato: {Id}, Tipo: {Tipo}, Salário: {Salario}, Início: {DataInicio}, Fim: {DataFim}, Ativo: {Ativo}";
        }
    }
}
