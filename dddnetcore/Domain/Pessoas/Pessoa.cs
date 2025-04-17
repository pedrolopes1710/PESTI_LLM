using System;
using dddnetcore.Domain.Contratos;
using DDDSample1.Domain.Shared;

namespace dddnetcore.Domain.Pessoas
{
    public class Pessoa : Entity<PessoaId>, IAggregateRoot
    {        
        public NomePessoa Nome { get; private set; }
        public EmailPessoa Email { get; private set; }
        public PessoaCienciaId CienciaId { get; private set; }
        public PessoaUltimoPedPagam UltimoPedidoPagamento { get; private set; }

        public Contrato Contrato { get; private set; }

        public Pessoa(NomePessoa nome, EmailPessoa email, PessoaCienciaId cienciaId, PessoaUltimoPedPagam ultimoPedidoPagamento, Contrato contrato)
        {
            this.Id = new PessoaId(Guid.NewGuid());
            Nome = nome;
            Email = email;
            CienciaId = cienciaId;
            UltimoPedidoPagamento = ultimoPedidoPagamento;
            Contrato = contrato;
        }


        // Métodos de alteração
        public void AlterarNome(NomePessoa novoNome)
        {
            if (novoNome == null) throw new ArgumentNullException(nameof(novoNome));
            Nome = novoNome;
        }

        public void AlterarEmail(EmailPessoa novoEmail)
        {
            if (novoEmail == null) throw new ArgumentNullException(nameof(novoEmail));
            Email = novoEmail;
        }

        public void AlterarCienciaId(PessoaCienciaId novaCienciaId)
        {
            if (novaCienciaId == null) throw new ArgumentNullException(nameof(novaCienciaId));
            CienciaId = novaCienciaId;
        }

        public void AlterarUltimoPedidoPagamento(PessoaUltimoPedPagam novoUltimoPedidoPagam)
        {
            if (novoUltimoPedidoPagam == null) throw new ArgumentNullException(nameof(novoUltimoPedidoPagam));
            UltimoPedidoPagamento = novoUltimoPedidoPagam;
        }

        public void AlterarContrato(Contrato novoContrato)
        {
            if (novoContrato == null) throw new ArgumentNullException(nameof(novoContrato));
            Contrato = novoContrato;
        }
    }
}
