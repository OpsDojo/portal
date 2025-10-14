// <copyright file="FeatureExtensions.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Host.Startup;

using Microsoft.Extensions.DependencyInjection;
using Portal.Application.Weather;
using Portal.Infrastructure;

/// <summary>
/// Extensions for adding features.
/// </summary>
public static class FeatureExtensions
{
    /// <summary>
    /// Adds the weather feature.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The original parameter, for chainable calls.</returns>
    public static IServiceCollection AddWeatherFeature(this IServiceCollection services)
    {
        services.AddScoped<IForecastRepo, InMemForecastRepo>();
        services.AddScoped<IForecastService, ForecastService>();

        return services;
    }
}
