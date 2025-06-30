using System;
using dddnetcore.Domain.TiposEntregavel;

namespace dddnetcore.Domain.Entregaveis
{
    public class EntregavelDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public string Descricao { get; set; }

        public DateTime Data { get; set; }

        public TipoEntregavelDto TipoEntregavel { get; set; }

        public Guid AtividadeId { get; set; }

        public EntregavelDto() { }

        public EntregavelDto(Entregavel entregavel)
        {
            this.Id = entregavel.Id.AsGuid();
            this.Nome = entregavel.Nome.Nome;
            this.Descricao = entregavel.Descricao.Descricao;
            this.Data = entregavel.Data.Data;
            this.TipoEntregavel = new TipoEntregavelDto(entregavel.TipoEntregavel);
            this.AtividadeId = entregavel.AtividadeId.AsGuid();
        }
    }
}