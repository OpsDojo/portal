﻿// <copyright file="EfWeightLogRepo.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Infrastructure.EF.Repositories;

using Portal.Application.Weight;
using Portal.Domain.Entities;

/// <inheritdoc cref="IWeightLogRepository"/>
public class EfWeightLogRepo : IWeightLogRepository
{
    /// <inheritdoc/>
    public Task<(IReadOnlyList<WeightLog>, int total)> GetPageAsync(
        Guid userId, int pageNumber, int pageSize, CancellationToken ct = default)
            => throw new NotImplementedException();
}
