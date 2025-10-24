// <copyright file="ForecastsController.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Host.Controllers;

using Microsoft.AspNetCore.Mvc;
using Portal.Application.Weather;
using Portal.Domain.Entities;

/// <summary>
/// Weather forecast controller.
/// </summary>
[ApiController]
[Route("[controller]")]
public class ForecastsController(IForecastService forecastService) : ControllerBase
{
    /// <summary>
    /// Gets the weather forecast.
    /// </summary>
    /// <returns>Weather forecast.</returns>
    [HttpGet]
    public IList<Forecast> Get() => forecastService.GetForecast();
}
