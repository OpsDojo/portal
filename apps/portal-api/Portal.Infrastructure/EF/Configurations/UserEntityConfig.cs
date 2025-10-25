// <copyright file="UserEntityConfig.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Infrastructure.EF.Configurations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Entities;

/// <summary>
/// User entity configuration.
/// </summary>
public class UserEntityConfig : IEntityTypeConfiguration<User>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<User> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        builder.ToTable("Users");
        builder.HasKey(s => s.Id);
        builder.HasIndex(s => s.Subject).IsUnique();
        builder.Property(s => s.Id).ValueGeneratedNever();
        builder.Property(s => s.Subject).IsRequired().HasMaxLength(60);
        builder.Property(s => s.DisplayName).HasMaxLength(40);
    }
}