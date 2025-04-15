using System;
using Xunit;
using FluentAssertions;
using dddnetcore.Domain.Entregaveis;

public class EntregavelIdTests
{
    [Fact]
    public void Constructor_ShouldCreateEntregavelId_WhenValidGuidIsProvided()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        var entregavelId = new EntregavelId(guid);

        // Assert
        entregavelId.AsGuid().Should().Be(guid);
    }

    [Fact]
    public void Constructor_ShouldCreateEntregavelId_WhenValidStringIsProvided()
    {
        // Arrange
        var guid = Guid.NewGuid();
        string guidString = guid.ToString();

        // Act
        var entregavelId = new EntregavelId(guidString);

        // Assert
        entregavelId.AsString().Should().Be(guidString);
        entregavelId.AsGuid().Should().Be(guid);
    }

    [Fact]
    public void AsString_ShouldReturnCorrectString()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var entregavelId = new EntregavelId(guid);

        // Act
        var result = entregavelId.AsString();

        // Assert
        result.Should().Be(guid.ToString());
    }

    [Fact]
    public void AsGuid_ShouldReturnCorrectGuid()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var entregavelId = new EntregavelId(guid);

        // Act
        var result = entregavelId.AsGuid();

        // Assert
        result.Should().Be(guid);
    }
}
