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
        public Guid Id { get; set; }
        public double PMs { get; set; }
        //public AfetacaoPerfilDto AfetacaoPerfil { get; set; }
        public CargaMensalDto CargaMensal { get; set; }

        public AfetacaoMensalDto() {}

        public AfetacaoMensalDto(AfetacaoMensal afetacaoMensal) {
            this.Id = afetacaoMensal.Id.AsGuid();
            this.PMs = afetacaoMensal.PMs.Quantidade;
            //this.AfetacaoPerfil = new AfetacaoPerfilDto(afetacaoMensal.AfetacaoPerfil);
            this.CargaMensal = new CargaMensalDto(afetacaoMensal.CargaMensal);
        }
    }
}