// <copyright file="UserService.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Application.System;

using Portal.Domain.Entities;

/// <inheritdoc cref="IUserService"/>
public class UserService(IUserRepository userRepo) : IUserService
{
    /// <inheritdoc/>
    public async Task<User> AddIfMissing(string email, string displayName, CancellationToken ct = default)
    {
        var user = await userRepo.GetByEmailAsync(email, ct);
        if (user == null)
        {
            user = new User(displayName, email);
            await userRepo.CreateAsync(user, ct);
        }

        return user;
    }
}
