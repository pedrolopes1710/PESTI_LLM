using System;
using Xunit;
using FluentAssertions;
using dddnetcore.Domain.Rubricas;
using DDDSample1.Domain.Shared;

public class RubricaTests
{
    [Fact]
    public void Constructor_ShouldCreateRubrica_WhenValidNameIsProvided()
    {
        // Arrange
        string nome = "Rubrica Teste";

        // Act
        var rubrica = new Rubrica(nome);

        // Assert
        rubrica.Id.Should().NotBeNull();
        rubrica.Nome.Should().NotBeNull();
        rubrica.Nome.Nome.Should().Be(nome);
    }

    [Fact]
    public void Constructor_ShouldThrowException_WhenInvalidNameIsProvided()
    {
        // Arrange
        string invalidName = "";

        // Act
        Action act = () => new Rubrica(invalidName);

        // Assert
        act.Should().Throw<BusinessRuleValidationException>()
            .WithMessage("The name cannot be null or empty!");
    }
}
