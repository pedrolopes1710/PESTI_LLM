using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dddnetcore.Domain.Orcamentos
{
    public class CreatingAfetacaoPerfilDto
    {
        public int DuracaoMes {get;set;}
        public double PMsAprovados {get;set;}
        public Guid PerfilId {get;set;}    
        public Guid PessoaId {get;set;}
    }
}