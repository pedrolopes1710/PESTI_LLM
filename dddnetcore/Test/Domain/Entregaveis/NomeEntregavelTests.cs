using System;
using Xunit;
using FluentAssertions;
using dddnetcore.Domain.Entregaveis;
using DDDSample1.Domain.Shared;

public class NomeEntregavelTests
{
    [Fact]
    public void Constructor_ShouldCreateNomeEntregavel_WhenValidNameIsProvided()
    {
        // Arrange
        string validName = "Entregável A";

        // Act
        var nomeEntregavel = new NomeEntegavel(validName);

        // Assert
        nomeEntregavel.Nome.Should().Be(validName);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Constructor_ShouldThrowException_WhenNameIsNullOrEmpty(string invalidName)
    {
        // Act
        Action act = () => new NomeEntegavel(invalidName);

        // Assert
        act.Should().Throw<BusinessRuleValidationException>()
            .WithMessage("The name cannot be null or empty!");
    }

    [Fact]
    public void Equals_ShouldReturnTrue_WhenNamesAreTheSame()
    {
        // Arrange
        var nome1 = new NomeEntegavel("Entregável A");
        var nome2 = new NomeEntegavel("Entregável A");

        // Act & Assert
        nome1.Should().Be(nome2);
    }

    [Fact]
    public void Equals_ShouldReturnFalse_WhenNamesAreDifferent()
    {
        // Arrange
        var nome1 = new NomeEntegavel("Entregável A");
        var nome2 = new NomeEntegavel("Entregável B");

        // Act & Assert
        nome1.Should().NotBe(nome2);
    }

    [Fact]
    public void GetHashCode_ShouldReturnSameValue_WhenNamesAreEqual()
    {
        // Arrange
        var nome1 = new NomeEntegavel("Entregável A");
        var nome2 = new NomeEntegavel("Entregável A");

        // Act & Assert
        nome1.GetHashCode().Should().Be(nome2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_ShouldReturnSameValue_WhenNamesAreDifferent()
    {
        // Arrange
        var nome1 = new NomeEntegavel("Entregável A");
        var nome2 = new NomeEntegavel("Entregável B");
        // Act & Assert
        nome1.GetHashCode().Should().NotBe(nome2.GetHashCode());
    }
}
