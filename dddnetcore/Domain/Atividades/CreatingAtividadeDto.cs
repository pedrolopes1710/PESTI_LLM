using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dddnetcore.Domain.Orcamentos
{
    public class CreatingAtividadeDto
    {
        public DateTime DataInicioAtividade {get;set;}
        public DateTime DataFimAtividade {get;set;}
        public string DescricaoAtividade {get;set;}
        public string NomeAtividade {get;set;}
        public Guid TarefaId {get;set;}
    }
}