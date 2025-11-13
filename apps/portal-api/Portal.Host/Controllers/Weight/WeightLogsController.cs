// <copyright file="WeightLogsController.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Host.Controllers.Weight;

using Microsoft.AspNetCore.Mvc;
using Portal.Application.Common;
using Portal.Application.System;
using Portal.Application.Weight;
using Portal.Domain.Entities;

/// <summary>
/// Controller for weight logs management.
/// </summary>
[ApiController]
[Route("weight/logs")]
public class WeightLogsController(IUserContext userContext, IWeightLogService logService) : ControllerBase
{
    /// <summary>
    /// Gets a specific page of weight log records for the current user.
    /// </summary>
    /// <param name="pageNumber">The page number.</param>
    /// <param name="pageSize">The page size.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>The page of records and the total.</returns>
    [HttpGet]
    public async Task<ActionResult<PagedResult<WeightLog>>> GetLogsAsync(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        var user = await userContext.GetUserAsync(ct);
        var result = await logService.GetLogsAsync(user.Id, pageNumber, pageSize, ct);

        return this.Ok(result);
    }

    /// <summary>
    /// Adds a new weight log for the current user.
    /// </summary>
    /// <param name="log">The log.</param>
    /// <param name="ct">The cancellation token.</param>
    /// <returns>Async task.</returns>
    [HttpPost]
    public async Task<ActionResult> AddLogAsync(
        [FromBody] WeightLogDto log,
        CancellationToken ct = default)
    {
        ArgumentNullException.ThrowIfNull(log);

        var user = await userContext.GetUserAsync(ct);
        var record = new WeightLog(log.Date, new(log.Kg), user.Id);
        await logService.AddLogAsync(record, ct);

        return this.Ok();
    }
}
