// <copyright file="IUserRepository.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Application.System;

using Portal.Domain.Entities;

/// <summary>
/// Repository for users.
/// </summary>
public interface IUserRepository
{
    /// <summary>
    /// Gets a user by the "sub" claim in their access token.
    /// </summary>
    /// <param name="email">The user email address.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A user, or null.</returns>
    public Task<User?> GetBySubjectClaim(string email, CancellationToken ct = default);

    /// <summary>
    /// Adds a new user.
    /// </summary>
    /// <param name="user">The user.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>Async task.</returns>
    public Task CreateAsync(User user, CancellationToken ct = default);
}
