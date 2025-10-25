// <copyright file="EfUserRepo.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Infrastructure.EF.Repositories;

using System.Threading;
using System.Threading.Tasks;
using Portal.Application.System;
using Portal.Domain.Entities;

/// <inheritdoc cref="IUserRepository"/>
public class EfUserRepo : IUserRepository
{
    /// <inheritdoc/>
    public Task CreateAsync(User user, CancellationToken ct = default)
        => throw new NotImplementedException();

    /// <inheritdoc/>
    public Task<User?> GetBySubjectClaim(string subject, CancellationToken ct = default)
        => throw new NotImplementedException();
}
