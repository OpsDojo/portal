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

    /// <summary>
    /// Gets the current application user.
    /// </summary>
    public User AppUser => this.appUser ??= this.LoadUser();

    private User LoadUser()
    {
        var subject = httpAccessor.HttpContext?.User.FindFirst("sub")?.Value;
        var name = httpAccessor.HttpContext?.User.FindFirst("name")?.Value;

        var user = string.IsNullOrEmpty(subject)
            ? throw new InvalidOperationException("Unable to determine user claims.")
            : userService.AddIfMissing(subject, name).GetAwaiter().GetResult();

        this.appUser = user;
        return user;
    }
}
