// <copyright file="IUserService.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Application.System;

using Portal.Domain.Entities;

/// <summary>
/// Service for logically handling users.
/// </summary>
public interface IUserService
{
    /// <summary>
    /// Adds a user if missing.
    /// </summary>
    /// <param name="subject">The subject to look for.</param>
    /// <param name="displayName">The display name to use if a new user is created.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A user.</returns>
    public Task<User> AddIfMissing(string subject, string? displayName, CancellationToken ct = default);
}
