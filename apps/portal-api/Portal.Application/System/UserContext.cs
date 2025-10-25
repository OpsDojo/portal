// <copyright file="UserContext.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Application.System;

using Microsoft.AspNetCore.Http;
using Portal.Domain.Entities;

/// <inheritdoc cref="IUserContext"/>
public class UserContext(IHttpContextAccessor httpAccessor, IUserService userService) : IUserContext
{
    private User? appUser;

    /// <inheritdoc/>
    public async Task<User> GetUserAsync(CancellationToken ct = default)
    {
        if (this.appUser is null)
        {
            var subject = httpAccessor.HttpContext?.User.FindFirst("sub")?.Value;
            var name = httpAccessor.HttpContext?.User.FindFirst("name")?.Value;

            this.appUser = string.IsNullOrEmpty(subject)
                ? throw new InvalidOperationException("Unable to determine user claims.")
                : await userService.AddIfMissing(subject, name, ct);
        }

        return this.appUser;
    }
}
