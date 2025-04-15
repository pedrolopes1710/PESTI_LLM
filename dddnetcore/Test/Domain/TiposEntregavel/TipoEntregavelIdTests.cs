using System;
using Xunit;
using FluentAssertions;
using dddnetcore.Domain.TiposEntregavel;

public class TipoEntregavelIdTests
{
    [Fact]
    public void Constructor_ShouldCreateTipoEntregavelId_WhenGuidIsProvided()
    {
        // Arrange
        var guid = Guid.NewGuid();

        // Act
        var tipoEntregavelId = new TipoEntregavelId(guid);

        // Assert
        tipoEntregavelId.AsGuid().Should().Be(guid);
    }

    [Fact]
    public void Constructor_ShouldCreateTipoEntregavelId_WhenStringGuidIsProvided()
    {
        // Arrange
        var guid = Guid.NewGuid();
        string guidString = guid.ToString();

        // Act
        var tipoEntregavelId = new TipoEntregavelId(guidString);

        // Assert
        tipoEntregavelId.AsString().Should().Be(guidString);
        tipoEntregavelId.AsGuid().Should().Be(guid);
    }

    [Fact]
    public void AsString_ShouldReturnCorrectString()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var tipoEntregavelId = new TipoEntregavelId(guid);

        // Act
        var result = tipoEntregavelId.AsString();

        // Assert
        result.Should().Be(guid.ToString());
    }

    [Fact]
    public void AsGuid_ShouldReturnCorrectGuid()
    {
        // Arrange
        var guid = Guid.NewGuid();
        var tipoEntregavelId = new TipoEntregavelId(guid);

        // Act
        var result = tipoEntregavelId.AsGuid();

        // Assert
        result.Should().Be(guid);
    }

    
}
