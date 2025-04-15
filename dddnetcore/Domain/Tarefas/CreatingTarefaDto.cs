using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Identity.Client;

namespace dddnetcore.Domain.Tarefas
{
    public class CreatingTarefaDto
    {
        public string Nome {get;set;}
        public string Descricao{get;set;}
        public string Status{get;set;}
    }
}