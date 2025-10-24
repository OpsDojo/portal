// <copyright file="PagedResult.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Application.Common;

/// <summary>
/// A paged result.
/// </summary>
/// <typeparam name="T">The item type.</typeparam>
/// <param name="Items">The items.</param>
/// <param name="TotalCount">The total count.</param>
/// <param name="PageNumber">The page number.</param>
/// <param name="PageSize">The page size.</param>
public record PagedResult<T>(IReadOnlyList<T> Items, int TotalCount, int PageNumber, int PageSize);