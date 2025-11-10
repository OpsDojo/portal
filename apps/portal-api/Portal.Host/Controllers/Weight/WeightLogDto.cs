// <copyright file="WeightLogDto.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Host.Controllers.Weight;

/// <summary>
/// A weight log data transfer object.
/// </summary>
public record WeightLogDto
{
    /// <summary>
    /// Gets the weight in kilograms.
    /// </summary>
    public required decimal Kg { get; init; }

    /// <summary>
    /// Gets the date of the log.
    /// </summary>
    public required DateOnly Date { get; init; }
}
