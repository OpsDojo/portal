// <copyright file="EfWeightLogRepo.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Infrastructure.EF.Repositories;

using Microsoft.EntityFrameworkCore;
using Portal.Application.Weight;
using Portal.Domain.Entities;

/// <inheritdoc cref="IWeightLogRepository"/>
public class EfWeightLogRepo(PortalDbContext db) : IWeightLogRepository
{
    /// <inheritdoc/>
    public async Task<(IReadOnlyList<WeightLog>, int total)> GetPageAsync(
        Guid userId,
        int pageNumber,
        int pageSize,
        CancellationToken ct = default)
    {
        var query = db.WeightLogs
            .Where(wl => wl.UserId == userId)
            .OrderByDescending(wl => wl.Date);

        var total = query.Count();
        var items = await query
            .Skip((pageNumber - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync(ct);

        return (items, total);
    }
}
