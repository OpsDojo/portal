// <copyright file="EfUserRepo.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Infrastructure.EF.Repositories;

using System.Threading;
using System.Threading.Tasks;
using Portal.Application.System;
using Portal.Domain.Entities;

/// <inheritdoc cref="IUserRepository"/>
public class EfUserRepo(PortalDbContext db) : IUserRepository
{
    /// <inheritdoc/>
    public async Task CreateAsync(User user, CancellationToken ct = default)
    {
        db.Users.Add(user);
        await db.SaveChangesAsync(ct);
    }

    /// <inheritdoc/>
    public async Task<User?> GetBySubjectClaim(string subject, CancellationToken ct = default)
        => await db.Users.FindAsync([subject], ct);
}
