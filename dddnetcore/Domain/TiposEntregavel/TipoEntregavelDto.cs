using System;

namespace dddnetcore.Domain.TiposEntregavel
{
    public class TipoEntregavelDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public TipoEntregavelDto() { }

        public TipoEntregavelDto(TipoEntregavel tipoEntregavel)
        {
            this.Id = tipoEntregavel.Id.AsGuid();
            this.Nome = tipoEntregavel.Nome.Nome;
        }
    }
}