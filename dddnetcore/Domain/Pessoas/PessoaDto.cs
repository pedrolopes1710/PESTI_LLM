using System;
using dddnetcore.Domain.Contratos;

namespace dddnetcore.Domain.Pessoas
{
    public class PessoaDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string PessoaCienciaId { get; set; }
        public DateTime PessoaUltimoPedPagam { get; set; }
        public ContratoDto Contrato { get; set; }

        public PessoaDto() {}

        public PessoaDto(Pessoa pessoa)
        {
            Id = pessoa.Id.AsGuid();
            Nome = pessoa.Nome.Value;
            Email = pessoa.Email.Value;
            PessoaCienciaId = pessoa.CienciaId.Value;
            PessoaUltimoPedPagam = pessoa.UltimoPedidoPagamento.Value;
            Contrato = new ContratoDto(pessoa.Contrato); // Supondo que tens um ContratoDto
        }
    }
}
