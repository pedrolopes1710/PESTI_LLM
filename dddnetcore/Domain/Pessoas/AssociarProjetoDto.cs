using System;
using System.Collections.Generic;


namespace dddnetcore.Domain.Pessoas
{
public class AssociarProjetoDto
{
    public string PessoaId { get; set; }
    public List<string> ProjetosIds { get; set; }
}
}
