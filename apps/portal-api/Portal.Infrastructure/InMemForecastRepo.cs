// <copyright file="InMemForecastRepo.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Infrastructure;

using Portal.Application.Weather;
using Portal.Domain.Entities;

/// <inheritdoc cref="IForecastRepo"/>
public class InMemForecastRepo : IForecastRepo
{
    private static readonly string[] Summaries =
    [
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering",
    ];

    /// <inheritdoc/>
    public IList<Forecast> GetForecast()
    {
        return [.. Enumerable.Range(1, 5).Select(index => new Forecast
        {
            Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
            TemperatureC = Random.Shared.Next(-20, 55),
            Summary = Summaries[Random.Shared.Next(Summaries.Length)],
        })];
    }
}
