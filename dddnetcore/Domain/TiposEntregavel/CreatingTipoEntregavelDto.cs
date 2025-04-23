using System;

namespace dddnetcore.Domain.TiposEntregavel
{
    public class CreatingTipoEntregavelDto
    {
        public string Nome { get; set; }

        public CreatingTipoEntregavelDto(string name)
        {
            this.Nome = name;
        }
    }
}