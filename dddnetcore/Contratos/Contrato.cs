using System;
using dddnetcore.Domain.Pessoas;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Contratos
{
    public class Contrato : Entity<ContratoId>, IAggregateRoot
    {
        public DataInicioContrato DataInicioContrato {get; private set;}

        public DataFimContrato DataFimContrato {get; private set;}

        public TipoContrato TipoContrato{get;private set;}
        public PessoaId PessoaId {get; private set;}


        private Contrato() {
        }

        public Contrato(DateTime dataIniciocontrato, DateTime dataFimcontrato,TipoContrato tipoContrato, PessoaId pessoaId) {
            this.Id = new ContratoId(Guid.NewGuid());
            this.DataInicioContrato = new DataInicioContrato(dataIniciocontrato);
            this.DataFimContrato = new DataFimContrato(dataFimcontrato);
            this.TipoContrato = tipoContrato;
            this.PessoaId=pessoaId;
        }
    }
}