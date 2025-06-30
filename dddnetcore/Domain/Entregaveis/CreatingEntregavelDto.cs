using System;
using dddnetcore.Domain.TiposEntregavel;


namespace dddnetcore.Domain.Entregaveis
{
    public class CreatingEntregavelDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public DateTime Data { get; set; }
        public TipoEntregavelDto TipoEntregavel { get; set; }

        public Guid AtividadeId { get; set; }

        public CreatingEntregavelDto(string name, string descricao, DateTime data, TipoEntregavelDto tipoEntregavel)
        {
            this.Nome = name;
            this.Descricao = descricao;
            this.Data = data;
            this.TipoEntregavel = tipoEntregavel;
            
        }
    }
}