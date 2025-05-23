using System;

namespace dddnetcore.Domain.Pessoas
{
    public class EditingPessoaDto
    {
        public string Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime PessoaUltimoPedPagam { get; set; }

    }
}
