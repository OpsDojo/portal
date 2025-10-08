// <copyright file="Forecast.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Domain.Weather;

/// <summary>
/// The weather forecast.
/// </summary>
public class Forecast
{
    /// <summary>
    /// Gets or sets the date.
    /// </summary>
    public DateOnly Date { get; set; }

    /// <summary>
    /// Gets or sets the temperature, in degrees Celcius.
    /// </summary>
    public int TemperatureC { get; set; }

    /// <summary>
    /// Gets the temperature, in degrees Fahrenheit.
    /// </summary>
    public int TemperatureF => 32 + (int)(this.TemperatureC * 9d / 5);

    /// <summary>
    /// Gets or sets the summary.
    /// </summary>
    public string? Summary { get; set; }
}
