// <copyright file="FeatureExtensions.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Host.Startup;

using Microsoft.Extensions.DependencyInjection;
using Portal.Application.System;
using Portal.Application.Weather;
using Portal.Application.Weight;
using Portal.Infrastructure;
using Portal.Infrastructure.EF.Repositories;

/// <summary>
/// Extensions for adding features.
/// </summary>
public static class FeatureExtensions
{
    /// <summary>
    /// Adds the system feature.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The original parameter, for chainable calls.</returns>
    public static IServiceCollection AddSystemFeature(this IServiceCollection services)
    {
        services.AddScoped<IUserRepository, EfUserRepo>();
        services.AddScoped<IUserService, UserService>();
        services.AddScoped<IUserContext, UserContext>();

        return services;
    }

    /// <summary>
    /// Adds the weight feature.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <returns>The original parameter, for chainable calls.</returns>
    public static IServiceCollection AddWeightFeature(this IServiceCollection services)
    {
        services.AddScoped<IWeightLogRepository, EfWeightLogRepo>();
        services.AddScoped<IWeightLogService, WeightLogService>();

        return services;
    }

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
