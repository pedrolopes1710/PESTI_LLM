using System;
using dddnetcore.Domain.ConsumoPerfis;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.ConsumoPerfis                  
{
    public class ConsumoPerfil : Entity<ConsumoPerfilId>, IAggregateRoot
    {
        public PMsConsumidos PMs {get; private set;}
     

        private ConsumoPerfil() {
        }

        public ConsumoPerfil(int pms) {
            this.Id = new ConsumoPerfilId(Guid.NewGuid());
            this.PMs=new PMsConsumidos(pms);
        }
    }
}