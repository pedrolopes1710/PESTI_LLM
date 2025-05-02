using System;

public class CreateProjetoDto
{
    public string Nome { get; set; }
    public string Descricao { get; set; }
}

public class ProjetoDTO
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }
}