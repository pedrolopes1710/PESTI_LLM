using System;
using dddnetcore.Domain.AfetacaoMensais;
using dddnetcore.Domain.AfetacaoPerfis;
using DDDSample1.Domain.Shared;


namespace dddnetcore.Domain.AfetacaoMensais
{
    public class AfetacaoMensal : Entity<AfetacaoMensalId>, IAggregateRoot
    {
        public PMs PMs {get;private set;}
        public AfetacaoPerfil AfetacaoPerfil {get;private set;}
        
        private AfetacaoMensal() {}

        public AfetacaoMensal(
            PMs pms,
            AfetacaoPerfil afetacaoPerfil)
        {
            this.Id = new AfetacaoMensalId(Guid.NewGuid()); 
            this.PMs = pms;   
            this.AfetacaoPerfil = afetacaoPerfil; 
        }

    }
}