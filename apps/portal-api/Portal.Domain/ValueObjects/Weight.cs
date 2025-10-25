// <copyright file="Weight.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Domain.ValueObjects;

/// <summary>
/// A weight unit.
/// </summary>
public enum WeightUnit
{
    /// <summary>
    /// Weight in kilograms.
    /// </summary>
    Kilograms,

    /// <summary>
    /// Weight in pounds.
    /// </summary>
    Pounds,

    /// <summary>
    /// Weight in stone.
    /// </summary>
    Stone,
}

/// <summary>
/// A weight measurement.
/// </summary>
/// <remarks>
/// Stored canonically in kilograms.
/// Supports arithmetic and comparison operators.
/// </remarks>
public sealed record Weight(decimal Kg) : IComparable<Weight>
{
    /// <summary>
    /// The number of kilograms in one pound.
    /// </summary>
    public const decimal KgPerPound = 0.45359237m;

    /// <summary>
    /// The number of kilograms in one stone.
    /// </summary>
    public const decimal KgPerStone = 6.35029318m;

    /// <summary>
    /// Creates a weight from kilograms.
    /// </summary>
    /// <param name="kg">The weight in kilograms.</param>
    /// <returns>A new <see cref="Weight"/> instance.</returns>
    public static Weight FromKg(decimal kg) => new(kg);

    /// <summary>
    /// Creates a weight from pounds.
    /// </summary>
    /// <param name="lbs">The weight in pounds.</param>
    /// <returns>A new <see cref="Weight"/> instance.</returns>
    public static Weight FromLbs(decimal lbs) => new(lbs * KgPerPound);

    /// <summary>
    /// Creates a weight from stone.
    /// </summary>
    /// <param name="stone">The weight in stone.</param>
    /// <returns>A new <see cref="Weight"/> instance.</returns>
    public static Weight FromStone(decimal stone) => new(stone * KgPerStone);

    /// <summary>
    /// Creates a weight from a value and unit.
    /// </summary>
    /// <param name="value">The weight value.</param>
    /// <param name="unit">The weight unit.</param>
    /// <returns>A new <see cref="Weight"/> instance.</returns>
    /// <exception cref="NotSupportedException">If an unknown unit is provided.</exception>
    public static Weight From(decimal value, WeightUnit unit) =>
        unit switch
        {
            WeightUnit.Kilograms => FromKg(value),
            WeightUnit.Pounds => FromLbs(value),
            WeightUnit.Stone => FromStone(value),
            _ => throw new NotSupportedException($"Unsupported weight unit: {unit}")
        };

    /// <summary>
    /// Gets the weight in pounds.
    /// </summary>
    public decimal Lbs => this.Kg / KgPerPound;

    /// <summary>
    /// Gets the weight in stone.
    /// </summary>
    public decimal Stone => this.Kg / KgPerStone;

    /// <summary>
    /// Adds two weights.
    /// </summary>
    /// <param name="left">The first weight.</param>
    /// <param name="right">The second weight.</param>
    /// <returns>The sum of the two weights.</returns>
    public static Weight operator +(Weight left, Weight right) => FromKg((left?.Kg ?? 0) + (right?.Kg ?? 0));

    /// <summary>
    /// Subtracts one weight from another.
    /// </summary>
    /// <param name="left">The first weight.</param>
    /// <param name="right">The second weight.</param>
    /// <returns>The difference between the two weights.</returns>
    public static Weight operator -(Weight left, Weight right) => FromKg((left?.Kg ?? 0) - (right?.Kg ?? 0));

    /// <summary>
    /// Compares two weights.
    /// </summary>
    /// <param name="left">The first weight.</param>
    /// <param name="right">The second weight.</param>
    /// <returns>True if left is greater than right.</returns>
    public static bool operator >(Weight left, Weight right) => (left?.Kg ?? 0) > (right?.Kg ?? 0);

    /// <summary>
    /// Compares two weights.
    /// </summary>
    /// <param name="left">The first weight.</param>
    /// <param name="right">The second weight.</param>
    /// <returns>True if left is less than right.</returns>
    public static bool operator <(Weight left, Weight right) => (left?.Kg ?? 0) < (right?.Kg ?? 0);

    /// <summary>
    /// Compares two weights.
    /// </summary>
    /// <param name="left">The first weight.</param>
    /// <param name="right">The second weight.</param>
    /// <returns>True if left is greater than or equal to right.</returns>
    public static bool operator >=(Weight left, Weight right) => (left?.Kg ?? 0) >= (right?.Kg ?? 0);

    /// <summary>
    /// Compares two weights.
    /// </summary>
    /// <param name="left">The first weight.</param>
    /// <param name="right">The second weight.</param>
    /// <returns>True if left is less than or equal to right.</returns>
    public static bool operator <=(Weight left, Weight right) => (left?.Kg ?? 0) <= (right?.Kg ?? 0);

    /// <inheritdoc/>
    public int CompareTo(Weight? other) => other is null ? 1 : this.Kg.CompareTo(other.Kg);

    /// <inheritdoc/>
    public override string ToString() => $"{this.Kg:N1} kg";
}
