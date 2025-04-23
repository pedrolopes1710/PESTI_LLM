using System;
using Xunit;
using FluentAssertions;
using dddnetcore.Domain.Entregaveis;
using dddnetcore.Domain.TiposEntregavel;
using DDDSample1.Domain.Shared;

public class EntregavelTests
{
    [Fact]
    public void Constructor_ShouldCreateEntregavel_WhenValidParametersAreProvided()
    {
        // Arrange
        string nome = "Nome do Entregável";
        string descricao = "Descrição do Entregável";
        DateTime data = DateTime.Now;
        TipoEntregavel tipo = new TipoEntregavel("Tipo A");

        // Act
        var entregavel = new Entregavel(nome, descricao, data, tipo);

        // Assert
        entregavel.Should().NotBeNull();
        entregavel.Nome.Nome.Should().Be(nome);
        entregavel.Descricao.Descricao.Should().Be(descricao);
        entregavel.Data.Data.Should().Be(data);
        entregavel.TipoEntregavel.Nome.Nome.Should().Be(tipo.Nome.Nome);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Constructor_ShouldThrowException_WhenNameIsNullOrEmpty(string invalidName)
    {
        // Arrange
        string descricao = "Descrição do Entregável";
        DateTime data = DateTime.Now;
        TipoEntregavel tipo = new TipoEntregavel("Tipo A");

        // Act
        Action act = () => new Entregavel(invalidName, descricao, data, tipo);

        // Assert
        act.Should().Throw<BusinessRuleValidationException>()
            .WithMessage("The name cannot be null or empty!");
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Constructor_ShouldThrowException_WhenDescriptionIsNullOrEmpty(string invalidDescription)
    {
        // Arrange
        string nome = "Nome do Entregável";
        DateTime data = DateTime.Now;
        TipoEntregavel tipo = new TipoEntregavel("Tipo A");

        // Act
        Action act = () => new Entregavel(nome, invalidDescription, data, tipo);

        // Assert
        act.Should().Throw<BusinessRuleValidationException>()
            .WithMessage("The description cannot be null or empty!");
    }

    [Theory]
    [InlineData("0001-01-01")]
    public void Constructor_ShouldThrowException_WhenDataIsMinValue(DateTime invalidDate)
    {
        // Arrange
        string nome = "Nome do Entregável";
        string descricao = "Descrição do Entregável";
        TipoEntregavel tipo = new TipoEntregavel("Tipo A");

        // Act
        Action act = () => new Entregavel(nome, descricao, invalidDate, tipo);

        // Assert
        act.Should().Throw<BusinessRuleValidationException>()
            .WithMessage("The date cannot be empty or uninitialized.");
    }

    [Fact]
    public void Id_ShouldBeGenerated_WhenEntregavelIsCreated()
    {
        // Arrange
        string nome = "Nome do Entregável";
        string descricao = "Descrição do Entregável";
        DateTime data = DateTime.Now;
        TipoEntregavel tipo = new TipoEntregavel("Tipo A");

        // Act
        var entregavel = new Entregavel(nome, descricao, data, tipo);

        // Assert
        entregavel.Id.AsGuid().Should().NotBe(Guid.Empty, "O Id não deve ser vazio.");
    }

    [Fact]
    public void TipoEntregavel_ShouldBeCorrectlyAssigned_WhenEntregavelIsCreated()
    {
        // Arrange
        string nome = "Nome do Entregável";
        string descricao = "Descrição do Entregável";
        DateTime data = DateTime.Now;
        TipoEntregavel tipo = new TipoEntregavel("Tipo A");

        // Act
        var entregavel = new Entregavel(nome, descricao, data, tipo);

        // Assert
        entregavel.TipoEntregavel.Should().NotBeNull();
        entregavel.TipoEntregavel.Nome.Nome.Should().Be(tipo.Nome.Nome);
    }

    [Fact]
    public void ChangeAttributes_ShouldUpdateFields()
    {
        // Arrange
        var entregavel = new Entregavel("Inicial", "Desc Inicial", DateTime.Today, new TipoEntregavel("InicialTipo"));

        var novoNome = "Atualizado";
        var novaDescricao = "Nova descrição";
        var novaData = new DateTime(2025, 12, 25);
        var novoTipo = new TipoEntregavel("NovoTipo");

        // Act
        entregavel.AlterarAtributos(novoNome, novaDescricao, novaData, novoTipo);

        // Assert
        entregavel.Nome.Nome.Should().Be(novoNome);
        entregavel.Descricao.Descricao.Should().Be(novaDescricao);
        entregavel.Data.Data.Should().Be(novaData);
        entregavel.TipoEntregavel.Nome.Nome.Should().Be("NovoTipo");
        }

}
