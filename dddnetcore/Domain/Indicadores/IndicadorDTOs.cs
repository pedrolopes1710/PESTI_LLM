using System;

public class CriarIndicadorDTO
{
    public string Nome { get; set; }
    public double ValorAtual { get; set; }
    public double ValorMaximo { get; set; }
    public Guid ProjetoId { get; set; }
}

public class IndicadorDTO
{
    public Guid Id { get; set; }
    public string Nome { get; set; }
    public double ValorAtual { get; set; }
    public double ValorMaximo { get; set; }
}