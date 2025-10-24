// <copyright file="PortalDbContext.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Infrastructure.EF;

using Microsoft.EntityFrameworkCore;
using Portal.Domain.Entities;

/// <summary>
/// The Portal database context.
/// </summary>
/// <param name="options">The db context options.</param>
public class PortalDbContext(DbContextOptions<PortalDbContext> options) : DbContext(options)
{
    /// <summary>
    /// Gets the users table.
    /// </summary>
    public virtual DbSet<User> Users { get; init; } = default!;

    /// <summary>
    /// Gets the weight logs table.
    /// </summary>
    public virtual DbSet<WeightLog> WeightLogs { get; init; } = default!;

    /// <inheritdoc/>
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        ArgumentNullException.ThrowIfNull(modelBuilder);

        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfigurationsFromAssembly(typeof(PortalDbContext).Assembly);
    }
}
