using System;

using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.AfetacaoPerfis
{
    public class AfetacaoPerfil : Entity<AfetacaoPerfilId>, IAggregateRoot
    {
        public DuracaoMes DuracaoMes {get;private set;}

        public PMsAprovados PMsAprovados {get;private set;}

        
        private AfetacaoPerfil() {}

        public AfetacaoPerfil(
            DuracaoMes duracaoMes,
            PMsAprovados pmsAprovados)
        {
            this.Id = new AfetacaoPerfilId(Guid.NewGuid()); 
            this.DuracaoMes = duracaoMes;
            this.PMsAprovados = pmsAprovados;   

        }

    }
}