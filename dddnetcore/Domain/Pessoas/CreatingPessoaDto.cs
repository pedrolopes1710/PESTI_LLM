using System;

namespace dddnetcore.Domain.Pessoas
{
    public class CreatingPessoaDto
    {
        public string Nome { get; set; }
        public string Email { get; set; }
        public string PessoaCienciaId { get; set; }
        public string PessoaUltimoPedPagam { get; set; }
        public Guid? ContratoId { get; set; }

    }
}
