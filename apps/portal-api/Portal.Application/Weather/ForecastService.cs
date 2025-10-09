// <copyright file="ForecastService.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Application.Weather;

using System.Collections.Generic;
using Portal.Domain.Weather;

/// <inheritdoc cref="IForecastService"/>
public class ForecastService(IForecastRepo forecastRepo) : IForecastService
{
    /// <inheritdoc/>
    public IList<Forecast> GetForecast() => forecastRepo.GetForecast();
}
