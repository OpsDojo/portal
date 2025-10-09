// <copyright file="IForecastRepo.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Application.Weather;

using Portal.Domain.Weather;

/// <summary>
/// Forecast repository.
/// </summary>
public interface IForecastRepo
{
    /// <summary>
    /// Gets the forecast.
    /// </summary>
    /// <returns>The forecast.</returns>
    public IList<Forecast> GetForecast();
}
