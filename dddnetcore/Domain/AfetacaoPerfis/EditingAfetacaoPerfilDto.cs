using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace dddnetcore.Domain.AfetacaoPerfis
{
    public class EditingAfetacaoPerfilDto
    {
        public Guid Id { get; set; }
        public int DuracaoMes { get; set; }
        public double PMsAprovados { get; set; }
        public Guid PerfilId { get; set; }
        public Guid PessoaId { get; set; }

    }
}