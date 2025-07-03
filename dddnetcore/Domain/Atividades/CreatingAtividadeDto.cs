using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dddnetcore.Domain.Atividades
{
    public class CreatingAtividadeDto
    {
        public DateTime DataInicioAtividade {get;set;}
        public DateTime DataFimAtividade {get;set;}
        public string DescricaoAtividade {get;set;}
        public string NomeAtividade {get;set;}
        public Guid? OrcamentoId {get;set;}
        public List<Guid> TarefasIds { get; set; }
        public List<Guid> EntregaveisIds { get; set; }
        public List<Guid> PerfisIds { get; set; }
        public List<Guid> OrcamentoIds { get; set; }
    }
}