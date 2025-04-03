using System;
using Xunit;
using FluentAssertions;
using dddnetcore.Domain.Rubricas;
using DDDSample1.Domain.Shared;

public class NomeRubricaTests
{
    [Fact]
    public void Constructor_ShouldCreateNomeRubrica_WhenValidNameIsProvided()
    {
        // Arrange
        string validName = "Rubrica A";

        // Act
        var nomeRubrica = new NomeRubrica(validName);

        // Assert
        nomeRubrica.Nome.Should().Be(validName);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Constructor_ShouldThrowException_WhenNameIsNullOrEmpty(string invalidName)
    {
        // Act
        Action act = () => new NomeRubrica(invalidName);

        // Assert
        act.Should().Throw<BusinessRuleValidationException>()
            .WithMessage("The name cannot be null or empty!");
    }

    [Fact]
    public void Equals_ShouldReturnTrue_WhenNamesAreTheSame()
    {
        // Arrange
        var nome1 = new NomeRubrica("Rubrica A");
        var nome2 = new NomeRubrica("Rubrica A");

        // Act & Assert
        nome1.Should().Be(nome2);
    }

    [Fact]
    public void Equals_ShouldReturnFalse_WhenNamesAreDifferent()
    {
        // Arrange
        var nome1 = new NomeRubrica("Rubrica A");
        var nome2 = new NomeRubrica("Rubrica B");

        // Act & Assert
        nome1.Should().NotBe(nome2);
    }

    [Fact]
    public void GetHashCode_ShouldReturnSameValue_WhenNamesAreEqual()
    {
        // Arrange
        var nome1 = new NomeRubrica("Rubrica A");
        var nome2 = new NomeRubrica("Rubrica A");

        // Act & Assert
        nome1.GetHashCode().Should().Be(nome2.GetHashCode());
    }
}
