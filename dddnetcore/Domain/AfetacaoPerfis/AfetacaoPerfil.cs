using System;
using dddnetcore.Domain.Perfis;
using dddnetcore.Domain.Pessoas;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.AfetacaoPerfis
{
    public class AfetacaoPerfil : Entity<AfetacaoPerfilId>, IAggregateRoot
    {
        public DuracaoMes DuracaoMes { get; private set; }

        public PMsAprovados PMsAprovados { get; private set; }

        public Perfil Perfil { get; private set; }
        public PerfilId PerfilId { get; private set; }

        public Pessoa Pessoa { get; private set; }
        public PessoaId PessoaId { get; private set; }


        private AfetacaoPerfil() { }

        public AfetacaoPerfil(
            DuracaoMes duracaoMes,
            PMsAprovados pmsAprovados,
            Perfil perfil,
            Pessoa pessoa)
        {
            this.Id = new AfetacaoPerfilId(Guid.NewGuid());
            this.DuracaoMes = duracaoMes;
            this.PMsAprovados = pmsAprovados;
            this.Perfil = perfil;
            this.Pessoa = pessoa;
        }
        public void ChangeDuracaoMes(DuracaoMes duracaoMes)
        {
            if (duracaoMes == null)
            {
                throw new BusinessRuleValidationException("DuracaoMes cannot be null.");
            }
            this.DuracaoMes = duracaoMes;
        }

        public void ChangePMsAprovados(PMsAprovados pmsAprovados)
        {
            if (pmsAprovados == null)
            {
                throw new BusinessRuleValidationException("PMsAprovados cannot be null.");
            }
            this.PMsAprovados = pmsAprovados;
        }
        public void ChangePerfilId(PerfilId perfilId)
        {
            if (perfilId == null)
            {
                throw new BusinessRuleValidationException("PerfilId cannot be null.");
            }
            this.PerfilId = perfilId;
        }
    }
}