using System;
using Xunit;
using FluentAssertions;
using dddnetcore.Domain.Entregaveis;
using DDDSample1.Domain.Shared;

public class DataEntregavelTests
{
    [Fact]
    public void Constructor_ShouldCreateDataEntregavel_WhenValidDateIsProvided()
    {
        // Arrange
        var validDate = new DateTime(2025, 4, 10);

        // Act
        var dataEntregavel = new DataEntregavel(validDate);

        // Assert
        dataEntregavel.Data.Should().Be(validDate);
    }

    [Theory]
    [InlineData("0001-01-01")]
    public void Constructor_ShouldThrowException_WhenDateIsMinValue(DateTime invalidDate)
    {
        // Act
        Action act = () => new DataEntregavel(invalidDate);

        // Assert
        act.Should().Throw<BusinessRuleValidationException>()
            .WithMessage("The date cannot be empty or uninitialized.");
    }
  
    [Fact]
    public void Equals_ShouldReturnTrue_WhenDatesAreTheSame()
    {
        // Arrange
        var sameDate = new DateTime(2025, 4, 10);

        var date1 = new DataEntregavel(sameDate);
        var date2 = new DataEntregavel(sameDate);

        // Act & Assert
        date1.Should().Be(date2);
    }

    [Fact]
    public void Equals_ShouldReturnFalse_WhenDatesAreDifferent()
    {
        // Arrange
        var differentDate1 = new DateTime(2025,2,2);
        var differentDate2 = new DateTime(2025,4,10);

        var date1 = new DataEntregavel(differentDate1);
        var date2 = new DataEntregavel(differentDate2);

        // Act & Assert
        date1.Should().NotBe(date2);
    }

    [Fact]
    public void GetHashCode_ShouldReturnSameValue_WhenDatesAreEqual()
    {
        // Arrange
        var sameDate = new DateTime(2025, 4, 10);

        var date1 = new DataEntregavel(sameDate);
        var date2 = new DataEntregavel(sameDate);

        // Act & Assert
        date1.GetHashCode().Should().Be(date2.GetHashCode());
    }

    [Fact]
    public void GetHashCode_ShouldReturnSameValue_WhenDatesAreDifferent()
    {
        // Arrange
        var differentDate1 = new DateTime(2025, 4, 10);
        var differentDate2 = new DateTime(2025, 7, 10);

        var date1 = new DataEntregavel(differentDate1);
        var date2 = new DataEntregavel(differentDate2);
        // Act & Assert
        date1.GetHashCode().Should().NotBe(date2.GetHashCode());
    }
}
