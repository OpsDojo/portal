// <copyright file="WeightLog.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Domain.Entities;

using Portal.Domain.ValueObjects;

/// <summary>
/// A weight log entity.
/// </summary>
public sealed record WeightLog
{
    /// <summary>
    /// Gets the entity id.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets the user id associated with this weight log.
    /// </summary>
    public Guid UserId { get; init; }

    /// <summary>
    /// Gets the user associated with this weight log.
    /// </summary>
    public User User { get; init; } = default!;

    /// <summary>
    /// Gets the date of the weight entry.
    /// </summary>
    public DateOnly Date { get; init; }

    /// <summary>
    /// Gets the recorded weight.
    /// </summary>
    public Weight Weight { get; init; }

    /// <summary>
    /// Gets any additional notes for this weight entry.
    /// </summary>
    public string? Notes { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="WeightLog"/> class.
    /// </summary>
    /// <param name="date">The date of the weight entry.</param>
    /// <param name="weight">The recorded weight.</param>
    /// <param name="userId">The user id associated.</param>
    /// <param name="notes">Optional notes for this weight entry.</param>
    /// <param name="id">Optional ID. If not provided, a new GUID is generated.</param>
    public WeightLog(DateOnly date, Weight weight, Guid userId, string? notes = null, Guid? id = null)
    {
        ArgumentNullException.ThrowIfNull(weight);

        this.Date = date;
        this.Weight = weight;
        this.UserId = userId;
        this.Notes = notes;
        this.Id = id ?? Guid.NewGuid();
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="WeightLog"/> class.
    /// </summary>
    private WeightLog()
    {
        this.Weight = default!;
    }
}
