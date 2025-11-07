// <copyright file="WeightLogService.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Application.Weight;

using Portal.Application.Common;
using Portal.Domain.Entities;

/// <inheritdoc cref="IWeightLogService"/>
public class WeightLogService(IWeightLogRepository logRepo) : IWeightLogService
{
    /// <inheritdoc/>
    public async Task<PagedResult<WeightLog>> GetLogsAsync(
        Guid userId, int pageNumber, int pageSize, CancellationToken ct = default)
    {
        var (page, total) = await logRepo.GetPageAsync(userId, pageNumber, pageSize, ct);
        return new PagedResult<WeightLog>(page, total, pageNumber, pageSize);
    }
}
