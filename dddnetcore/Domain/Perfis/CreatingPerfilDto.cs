using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace dddnetcore.Domain.Perfis
{
    public class CreatingPerfilDto
    {
        public int PMs{get;set;}
        public string Descricao{get;set;}

        public Guid TipoVinculoId{get;set;}
    }
}