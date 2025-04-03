using System;
using Xunit;
using FluentAssertions;
using dddnetcore.Domain.Rubricas;

public class RubricaIdTests
{
    [Fact]
    public void Constructor_ShouldCreateRubricaId_WhenValidGuidIsProvided()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        var rubricaId = new RubricaId(guid);

        // Assert
        rubricaId.AsGuid().Should().Be(guid);
    }

    [Fact]
    public void Constructor_ShouldCreateRubricaId_WhenValidStringIsProvided()
    {
        // Arrange
        var guid = Guid.NewGuid();
        string guidString = guid.ToString();

        // Act
        var rubricaId = new RubricaId(guidString);

        // Assert
        rubricaId.AsString().Should().Be(guidString);
        rubricaId.AsGuid().Should().Be(guid);
    }

    [Fact]
    public void AsString_ShouldReturnCorrectString()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var rubricaId = new RubricaId(guid);

        // Act
        var result = rubricaId.AsString();

        // Assert
        result.Should().Be(guid.ToString());
    }

    [Fact]
    public void AsGuid_ShouldReturnCorrectGuid()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var rubricaId = new RubricaId(guid);

        // Act
        var result = rubricaId.AsGuid();

        // Assert
        result.Should().Be(guid);
    }
}
