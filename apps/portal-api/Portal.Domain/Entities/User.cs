// <copyright file="User.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Domain.Entities;

using System.Diagnostics.CodeAnalysis;

/// <summary>
/// A user entity.
/// </summary>
public sealed record User
{
    /// <summary>
    /// Gets the entity id.
    /// </summary>
    public Guid Id { get; init; }

    /// <summary>
    /// Gets the display name.
    /// </summary>
    public string? DisplayName { get; init; }

    /// <summary>
    /// Gets the subject claim.
    /// </summary>
    public string Subject { get; init; }

    /// <summary>
    /// Gets the joined date.
    /// </summary>
    public DateTimeOffset Joined { get; init; }

    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class.
    /// </summary>
    /// <param name="subject">The claim subject.</param>
    /// <param name="displayName">The display name.</param>
    /// <param name="entityId">Optional entity id. If not provided, a new GUID is generated.</param>
    /// <param name="joined">Optional join date. If not provided, current UTC time is used.</param>
    /// <exception cref="ArgumentException">Thrown if <paramref name="subject"/> is null or whitespace.</exception>
    public User(
        string subject,
        string? displayName = null,
        Guid? entityId = null,
        DateTimeOffset? joined = null)
    {
        if (string.IsNullOrWhiteSpace(subject))
        {
            throw new ArgumentException("Subject cannot be empty.", nameof(subject));
        }

        this.Subject = subject;
        this.DisplayName = displayName;
        this.Id = entityId ?? Guid.NewGuid();
        this.Joined = joined ?? DateTimeOffset.UtcNow;
    }

    /// <summary>
    /// Initializes a new instance of the <see cref="User"/> class.
    /// </summary>
    [ExcludeFromCodeCoverage]
    private User()
    {
        this.Subject = default!;
    }

    /// <summary>
    /// Changes the display name of the user.
    /// </summary>
    /// <param name="newName">The new display name.</param>
    /// <returns>A new <see cref="User"/> instance with updated display name.</returns>
    /// <exception cref="ArgumentException">Thrown if <paramref name="newName"/> is null or whitespace.</exception>
    [SuppressMessage(
        "StyleCop.CSharp.ReadabilityRules",
        "SA1101:Prefix local calls with this",
        Justification = "StyleCop bug")]
    public User ChangeDisplayName(string newName)
        => this with { DisplayName = newName };
}