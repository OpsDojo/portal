// <copyright file="WeightLogsController.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Host.Controllers.Weight;

using Microsoft.AspNetCore.Mvc;
using Portal.Application.System;
using Portal.Application.Weight;

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
    public async Task<IActionResult> GetLogsAsync(
        [FromQuery] int pageNumber = 1,
        [FromQuery] int pageSize = 10,
        CancellationToken ct = default)
    {
        var userId = userContext.AppUser.Id;
        var result = await logService.GetLogsAsync(userId, pageNumber, pageSize, ct);

        return this.Ok(result);
    }
}
