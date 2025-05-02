using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.CargasMensais;

namespace dddnetcore.Domain.AfetacaoMensais
{
    public class CreatingAfetacaoMensalDto
    {
        public double PMs { get; set; }
        public Guid AfetacaoPerfilId { get; set; }
        public DateTime MesAno { get; set; }
    }
}