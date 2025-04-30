using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using dddnetcore.Domain.AfetacaoPerfis;
using dddnetcore.Domain.CargasMensais;

namespace dddnetcore.Domain.AfetacaoMensais
{
    public class AfetacaoMensalDto
    {
        public Guid Id { get; private set; }
        public PMs PMs { get; private set; }
        public AfetacaoPerfil AfetacaoPerfil { get; private set; }
        public CargaMensal CargaMensal { get; private set; }

        public AfetacaoMensalDto(AfetacaoMensal afetacaoMensal)
        {
            this.Id = afetacaoMensal.Id.AsGuid();
            this.PMs = afetacaoMensal.PMs;
            this.AfetacaoPerfil = afetacaoMensal.AfetacaoPerfil;
            this.CargaMensal = afetacaoMensal.CargaMensal;
        }
    }
}