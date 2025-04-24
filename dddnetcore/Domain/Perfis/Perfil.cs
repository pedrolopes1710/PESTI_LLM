using System;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Perfis
{
    public class Perfil : Entity<PerfilId>, IAggregateRoot
    {
        public PMsTotais PMs { get; private set; }
        public DescricaoPerfil DescricaoPerfil { get; private set; }

        private Perfil() { }

        public Perfil(int pms, string descricao)
        {
            Id = new PerfilId(Guid.NewGuid());
            PMs = new PMsTotais(pms);
            DescricaoPerfil = new DescricaoPerfil(descricao);
        }
        public void ChangeDescricao(DescricaoPerfil descricaoPerfil)
        {
            if (descricaoPerfil == null)
            {
                throw new BusinessRuleValidationException("Descrição do perfil não pode ser nula.");
            }
            this.DescricaoPerfil = descricaoPerfil;
        }
        public void ChangePMs(PMsTotais pms)
        {
            if (pms == null)
            {
                throw new BusinessRuleValidationException("PMs não pode ser nulo.");
            }
            this.PMs = pms;
        }
        

    }
}