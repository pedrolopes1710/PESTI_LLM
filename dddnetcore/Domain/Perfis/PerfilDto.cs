using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dddnetcore.Domain.Perfis
{
    public class PerfilDto
    {
        public Guid Id {get;set;}
        public int PMs{get;set;}

        public string Descricao{get;set;}  

        public PerfilDto() {}

        public PerfilDto(Perfil perfil)
        {
            this.Id = perfil.Id.AsGuid();
            this.PMs = perfil.PMs.Valor;
            this.Descricao=perfil.DescricaoPerfil.Valor;
        }
          
    }
}