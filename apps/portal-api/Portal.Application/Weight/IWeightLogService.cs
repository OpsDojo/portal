// <copyright file="IWeightLogService.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Application.Weight;

using Portal.Application.Common;
using Portal.Domain.Entities;

/// <summary>
/// Service for logically handling weight logs.
/// </summary>
public interface IWeightLogService
{
    /// <summary>
    /// Gets paged weight logs for a user.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="pageSize">The page size.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>A paged list.</returns>
    public Task<PagedResult<WeightLog>> GetLogsAsync(
        Guid userId, int pageNumber, int pageSize, CancellationToken ct = default);

    /// <summary>
    /// Adds a new weight log for a user.
    /// </summary>
    /// <param name="log">The weight log.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>Async task.</returns>
    public Task AddLogAsync(WeightLog log, CancellationToken ct = default);
}
