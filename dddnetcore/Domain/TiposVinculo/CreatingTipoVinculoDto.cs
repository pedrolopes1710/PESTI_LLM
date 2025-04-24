using System;

namespace dddnetcore.Domain.TiposVinculo
{
    public class CreatingTipoVinculoDto
    {
        public string Nome { get; set; }

        public CreatingTipoVinculoDto(string name)
        {
            this.Nome = name;
        }
    }
}