using System;
using dddnetcore.Domain.Atividades;
using dddnetcore.Domain.TiposVinculo;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Perfis
{
    public class Perfil : Entity<PerfilId>, IAggregateRoot
    {
        public PMsTotais PMs { get; private set; }
        public DescricaoPerfil DescricaoPerfil { get; private set; }

        public TipoVinculo TipoVinculo { get; private set; }
        public AtividadeId AtividadeId { get; private set; }

        private Perfil() { }

        public Perfil(int pms, string descricao, TipoVinculo tipoVinculo)
        {
            Id = new PerfilId(Guid.NewGuid());
            PMs = new PMsTotais(pms);
            DescricaoPerfil = new DescricaoPerfil(descricao);
            TipoVinculo = tipoVinculo;
            
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
        public void SetAtividadeId(AtividadeId atividadeId)
        {
            if (atividadeId == null)
                throw new BusinessRuleValidationException("AtividadeId não pode ser nulo.");
            this.AtividadeId = atividadeId;
        }

    }
}