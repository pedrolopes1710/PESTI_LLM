using System;
using System.Collections.Generic;
using System.Linq;
using dddnetcore.Domain.Contratos;
using dddnetcore.Domain.CargasMensais;
using dddnetcore.Domain.Projetos;
using dddnetcore.Controllers;


namespace dddnetcore.Domain.Pessoas
{
    public class PessoaDto
    {
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public string PessoaCienciaId { get; set; }
        public DateTime PessoaUltimoPedPagam { get; set; }
        public ContratoDto? Contrato { get; set; }
        public List<CreateProjetoDto> Projetos { get; set; } = new();
        public List<CargaMensalDto> CargasMensais { get; set; } = new();

        public PessoaDto() {}

        public PessoaDto(Pessoa pessoa)
        {
            Id = pessoa.Id.AsGuid();
            Nome = pessoa.Nome.Value;
            Email = pessoa.Email.Value;
            PessoaCienciaId = pessoa.CienciaId.Value;
            PessoaUltimoPedPagam = pessoa.UltimoPedidoPagamento.Value;
            
            if (pessoa.Contrato != null)
                Contrato = new ContratoDto(pessoa.Contrato);
            
            Projetos = pessoa.Projetos?.Select(p => new CreateProjetoDto {
                Nome = p.NomeProjeto.Valor,
                Descricao = p.DescricaoProjeto.Valor,
            }).ToList() ?? new();    

            CargasMensais = pessoa.CargasMensais?
                .Select(c => new CargaMensalDto(c)) // assumes CargaMensalDto has a constructor
                .ToList() ?? new();
        }
    }
}
