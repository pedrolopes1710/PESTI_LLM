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
    }
}