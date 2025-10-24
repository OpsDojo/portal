// <copyright file="InfraExtensions.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Host.Startup;

using Microsoft.Extensions.DependencyInjection;
using Portal.Infrastructure.EF;

/// <summary>
/// Extensions for adding infrastructure.
/// </summary>
public static class InfraExtensions
{
    /// <summary>
    /// Adds the system feature.
    /// </summary>
    /// <param name="services">The service collection.</param>
    /// <param name="config">The configuration.</param>
    /// <returns>The original parameter, for chainable calls.</returns>
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration config)
    {
        var connectionString = config.GetConnectionString("PortalDb");
        services.AddSqlServer<PortalDbContext>(connectionString, opts =>
        {
            opts.EnableRetryOnFailure(
                maxRetryCount: 5,
                maxRetryDelay: TimeSpan.FromSeconds(30),
                errorNumbersToAdd: null);
        });

        return services;
    }
}