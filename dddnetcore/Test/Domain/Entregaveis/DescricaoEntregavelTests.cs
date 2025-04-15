using System;
using Xunit;
using FluentAssertions;
using dddnetcore.Domain.Entregaveis;
using DDDSample1.Domain.Shared;

public class DescricaoEntregavelTests
{
    [Fact]
    public void Constructor_ShouldCreateDescricaoEntregavel_WhenValidDescricaoIsProvided()
    {
        // Arrange
        string validDescricao = "Descrição do Entregável A";

        // Act
        var descricaoEntregavel = new DescricaoEntregavel(validDescricao);

        // Assert
        descricaoEntregavel.Descricao.Should().Be(validDescricao);
    }

    [Theory]
    [InlineData(null)]
    [InlineData("")]
    public void Constructor_ShouldThrowException_WhenDescriptionIsNullOrEmpty(string invalidDescricao)
    {
        // Act
        Action act = () => new DescricaoEntregavel(invalidDescricao);

        // Assert
        act.Should().Throw<BusinessRuleValidationException>()
            .WithMessage("The description cannot be null or empty!");
    }

    [Fact]
    public void Equals_ShouldReturnTrue_WhenDescriptionsAreTheSame()
    {
        // Arrange
        var descricao1 = new DescricaoEntregavel("Descrição do Entregável A");
        var descricao2 = new DescricaoEntregavel("Descrição do Entregável A");

        // Act & Assert
        descricao1.Should().Be(descricao2);
    }

    [Fact]
    public void Equals_ShouldReturnFalse_WhenDescriptionsAreDifferent()
    {
        // Arrange
        var descricao1 = new DescricaoEntregavel("Descrição do Entregável A");
        var descricao2 = new DescricaoEntregavel("Descrição do Entregável B");

        // Act & Assert
        descricao1.Should().NotBe(descricao2);
    }

    [Fact]
    public void GetHashCode_ShouldReturnSameValue_WhenDescriptionsAreEqual()
    {
        // Arrange
        var descricao1 = new DescricaoEntregavel("Descrição do Entregável A");
        var descricao2 = new DescricaoEntregavel("Descrição do Entregável A");

        // Act & Assert
        descricao1.GetHashCode().Should().Be(descricao2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_ShouldReturnSameValue_WhenDescriptionsAreDifferent()
    {
        // Arrange
        var descricao1 = new DescricaoEntregavel("Descrição do Entregável A");
        var descricao2 = new DescricaoEntregavel("Descrição do Entregável B");
        // Act & Assert
        descricao1.GetHashCode().Should().NotBe(descricao2.GetHashCode());
    }
}
