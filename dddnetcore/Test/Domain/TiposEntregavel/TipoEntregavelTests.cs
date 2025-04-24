using System;
using Xunit;
using FluentAssertions;
using dddnetcore.Domain.TiposEntregavel;
using DDDSample1.Domain.Shared;

public class TipoEntregavelTests
{
    [Fact]
    public void Constructor_ShouldCreateTipoEntregavel_WhenNomeIsValid()
    {
        // Arrange
        string nome = "Tipo A";

        // Act
        var tipoEntregavel = new TipoEntregavel(nome);

        // Assert
        tipoEntregavel .Id.Should().NotBeNull();
        tipoEntregavel .Nome.Should().NotBeNull();
        tipoEntregavel .Nome.Nome.Should().Be(nome);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Constructor_ShouldThrowException_WhenNameIsNullOrEmpty(string invalidName)
    {
        // Act
        Action act = () => new TipoEntregavel(invalidName);

        // Assert
        act.Should().Throw<BusinessRuleValidationException>()
            .WithMessage("The name cannot be null or empty!");
    }
    
    [Fact]
    public void Id_ShouldBeGenerated_WhenTipoEntregavelIsCreated()
    {
        // Arrange
        string nome = "Tipo A";

        // Act
        var tipoEntregavel = new TipoEntregavel(nome);

        // Assert
        tipoEntregavel.Id.AsGuid().Should().NotBe(Guid.Empty, "O Id não deve ser vazio.");
    }

       [Fact]
    public void ChangeAttributes_ShouldUpdateFields()
    {
        // Arrange
        var tipoEntregavel = new TipoEntregavel("Tipo A");

        var novoNome = "Atualizado";

        // Act
        tipoEntregavel.AlterarAtributos(novoNome);

        // Assert
        tipoEntregavel.Nome.Nome.Should().Be(novoNome);
    }

}

