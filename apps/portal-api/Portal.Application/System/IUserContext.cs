// <copyright file="IUserContext.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Application.System;

using Portal.Domain.Entities;

/// <summary>
/// The user context.
/// </summary>
public interface IUserContext
{
    /// <summary>
    /// Gets the current application user.
    /// </summary>
    public User AppUser { get; }
}
