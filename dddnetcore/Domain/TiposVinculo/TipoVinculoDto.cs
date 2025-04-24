using System;

namespace dddnetcore.Domain.TiposVinculo
{
    public class TipoVinculoDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }

        public TipoVinculoDto() { }

        public TipoVinculoDto(TipoVinculo tipoVinculo)
        {
            this.Id = tipoVinculo.Id.AsGuid();
            this.Nome = tipoVinculo.Nome.Nome;
        }
    }
}