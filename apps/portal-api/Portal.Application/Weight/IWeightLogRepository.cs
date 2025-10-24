// <copyright file="IWeightLogRepository.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Application.Weight;

using Portal.Domain.Entities;

/// <summary>
/// Repository for weight logs.
/// </summary>
public interface IWeightLogRepository
{
    /// <summary>
    /// Gets a specific page of weight log records for a user.
    /// </summary>
    /// <param name="userId">The user id.</param>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="pageSize">The page size.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The page of records and the total.</returns>
    public Task<(IReadOnlyList<WeightLog>, int total)> GetPageAsync(
        Guid userId, int pageNumber, int pageSize, CancellationToken ct = default);
}
