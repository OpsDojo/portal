// <copyright file="WeightTests.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Domain.Tests.ValueObjects;

using Portal.Domain.ValueObjects;

/// <summary>
/// Tests for the <see cref="Weight"/> value object.
/// </summary>
public class WeightTests
{
    /// <summary>
    /// Tests that weights can be created from kilograms.
    /// </summary>
    [Fact]
    public void FromKg_WhenCalled_ShouldMakeExpected()
    {
        // Arrange
        const decimal kg = 70.0m;

        // Act
        var weight = Weight.From(kg, WeightUnit.Kilograms);

        // Assert
        weight.Kg.ShouldBe(kg);
    }

    [Fact]
    public void FromLbs_WhenCalled_ShouldMakeExpected()
    {
        // Arrange
        const decimal lbs = 154.324m;
        const decimal expectedKg = lbs * Weight.KgPerPound;

        // Act
        var weight = Weight.From(lbs, WeightUnit.Pounds);

        // Assert
        weight.Kg.ShouldBe(expectedKg);
    }

    [Fact]
    public void FromStone_WhenCalled_ShouldMakeExpected()
    {
        // Arrange
        const decimal stone = 11.0m;
        const decimal expectedKg = stone * Weight.KgPerStone;

        // Act
        var weight = Weight.From(stone, WeightUnit.Stone);

        // Assert
        weight.Kg.ShouldBe(expectedKg);
    }

    [Fact]
    public void FromUnknownUnit_WhenCalled_ShouldThrow()
    {
        // Arrange
        const decimal value = 100.0m;
        const WeightUnit unknownUnit = (WeightUnit)999;

        // Act
        var act = () => Weight.From(value, unknownUnit);

        // Assert
        act.ShouldThrow<NotSupportedException>();
    }

    [Theory]
    [InlineData(50.0, 20.0, 70.0)]
    [InlineData(null, 20.0, 20.0)]
    [InlineData(50.0, null, 50.0)]
    [InlineData(null, null, 0.0)]
    public void SumOperator_WhenCalled_ShouldAddWeights(double? weightA, double? weightB, decimal expected)
    {
        // Arrange
        var weight1 = weightA.HasValue ? Weight.FromKg((decimal)weightA) : null;
        var weight2 = weightB.HasValue ? Weight.FromKg((decimal)weightB) : null;
        var expectedWeight = Weight.FromKg(expected);

        // Act
        var result = weight1! + weight2!;

        // Assert
        result.ShouldBe(expectedWeight);
    }

    [Theory]
    [InlineData(70.0, 20.0, 50.0)]
    [InlineData(null, 20.0, -20.0)]
    [InlineData(50.0, null, 50.0)]
    [InlineData(null, null, 0.0)]
    public void SubtractOperator_WhenCalled_ShouldSubtractWeights(double? weightA, double? weightB, decimal expected)
    {
        // Arrange
        var weight1 = weightA.HasValue ? Weight.FromKg((decimal)weightA) : null;
        var weight2 = weightB.HasValue ? Weight.FromKg((decimal)weightB) : null;
        var expectedWeight = Weight.FromKg(expected);

        // Act
        var result = weight1! - weight2!;

        // Assert
        result.ShouldBe(expectedWeight);
    }

    [Theory]
    [InlineData(50.0, 50.0, false)]
    [InlineData(50.0, 70.0, true)]
    [InlineData(70.0, 50.0, false)]
    [InlineData(50.0, null, false)]
    [InlineData(null, 50.0, true)]
    [InlineData(null, null, false)]
    public void LessThanOperator_WhenCalled_ShouldReturnExpected(double? basis, double? compareTo, bool expected)
    {
        // Arrange
        var baseWeight = basis.HasValue ? Weight.FromKg((decimal)basis) : null;
        var comparison = compareTo.HasValue ? Weight.FromKg((decimal)compareTo) : null;

        // Act & Assert
        (baseWeight! < comparison!).ShouldBe(expected);
    }

    [Theory]
    [InlineData(50.0, 50.0, true)]
    [InlineData(50.0, 70.0, true)]
    [InlineData(70.0, 50.0, false)]
    [InlineData(50.0, null, false)]
    [InlineData(null, 50.0, true)]
    [InlineData(null, null, true)]
    public void LessThanOrEqualToOperator_WhenCalled_ShouldReturnExpected(
        double? basis, double? compareTo, bool expected)
    {
        // Arrange
        var baseWeight = basis.HasValue ? Weight.FromKg((decimal)basis) : null;
        var comparison = compareTo.HasValue ? Weight.FromKg((decimal)compareTo) : null;

        // Act & Assert
        (baseWeight! <= comparison!).ShouldBe(expected);
    }

    [Theory]
    [InlineData(50.0, 50.0, false)]
    [InlineData(50.0, 70.0, false)]
    [InlineData(70.0, 50.0, true)]
    [InlineData(50.0, null, true)]
    [InlineData(null, 50.0, false)]
    [InlineData(null, null, false)]
    public void GreaterThanOperator_WhenCalled_ShouldReturnExpected(double? basis, double? compareTo, bool expected)
    {
        // Arrange
        var baseWeight = basis.HasValue ? Weight.FromKg((decimal)basis) : null;
        var comparison = compareTo.HasValue ? Weight.FromKg((decimal)compareTo) : null;

        // Act & Assert
        (baseWeight! > comparison!).ShouldBe(expected);
    }

    [Theory]
    [InlineData(50.0, 50.0, true)]
    [InlineData(50.0, 70.0, false)]
    [InlineData(70.0, 50.0, true)]
    [InlineData(50.0, null, true)]
    [InlineData(null, 50.0, false)]
    [InlineData(null, null, true)]
    public void GreaterThanOrEqualToOperator_WhenCalled_ShouldReturnExpected(
        double? basis, double? compareTo, bool expected)
    {
        // Arrange
        var baseWeight = basis.HasValue ? Weight.FromKg((decimal)basis) : null;
        var comparison = compareTo.HasValue ? Weight.FromKg((decimal)compareTo) : null;

        // Act & Assert
        (baseWeight! >= comparison!).ShouldBe(expected);
    }

    [Fact]
    public void ToString_WhenCalled_ShouldReturnExpectedFormat()
    {
        // Arrange
        var weight = Weight.FromKg(70.0m);
        const string expectedString = "70.0 kg";

        // Act
        var result = weight.ToString();

        // Assert
        result.ShouldBe(expectedString);
    }

    [Theory]
    [InlineData(70.0, 0)]
    [InlineData(50.0, 1)]
    [InlineData(90.0, -1)]
    [InlineData(null, 1)]
    public void CompareTo70Kg_WhenCalled_ShouldCompareWeights(double? compare, int expected)
    {
        // Arrange
        var weightA = Weight.FromKg(70);
        var weightB = compare.HasValue ? Weight.FromKg((decimal)compare) : null;

        // Act
        var result = weightA.CompareTo(weightB);

        // Assert
        result.ShouldBe(expected);
    }

    [Fact]
    public void OtherUnits_WhenCalled_ShouldReturnExpectedValues()
    {
        // Arrange
        var weight = Weight.FromKg(70.0m);
        const decimal expectedLbs = 70.0m / Weight.KgPerPound;
        const decimal expectedStone = 70.0m / Weight.KgPerStone;

        // Act
        var lbs = weight.Lbs;
        var stone = weight.Stone;

        // Assert
        lbs.ShouldBe(expectedLbs);
        stone.ShouldBe(expectedStone);
    }
}
