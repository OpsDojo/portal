// <copyright file="WeightLogEntityConfig.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Infrastructure.EF.Configurations;

using System.Globalization;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Portal.Domain.Entities;
using Portal.Domain.ValueObjects;

/// <summary>
/// Weight log entity configuration.
/// </summary>
public class WeightLogEntityConfig : IEntityTypeConfiguration<WeightLog>
{
    /// <inheritdoc/>
    public void Configure(EntityTypeBuilder<WeightLog> builder)
    {
        ArgumentNullException.ThrowIfNull(builder);

        var kgPerPound = Weight.KgPerPound.ToString(CultureInfo.InvariantCulture);
        var kgPerStone = Weight.KgPerStone.ToString(CultureInfo.InvariantCulture);

        builder.ToTable("WeightLogs");
        builder.HasKey(s => s.Id);
        builder.HasIndex(s => new { s.UserId, s.Date }).IsUnique();
        builder.HasIndex(s => s.UserId);
        builder.Property(s => s.Id).ValueGeneratedNever();
        builder.Property(s => s.Date).IsRequired();
        builder.Property(s => s.Notes).HasMaxLength(250);

        builder.OwnsOne(w => w.Weight, weight =>
        {
            weight.Property(wt => wt.Kg)
                .HasColumnName("WeightKg")
                .HasColumnType("decimal(10,6)")
                .IsRequired();
            weight.Property<decimal>("WeightLbs")
                .HasColumnName("WeightLbs")
                .HasColumnType("decimal(10,6)")
                .HasComputedColumnSql($"CAST([WeightKg] / {kgPerPound} AS decimal(10,6))", stored: true);
            weight.Property<decimal>("WeightStone")
                .HasColumnName("WeightStone")
                .HasColumnType("decimal(10,6)")
                .HasComputedColumnSql($"CAST([WeightKg] / {kgPerStone} AS decimal(10,6))", stored: true);
        });

        builder.HasOne(w => w.User)
            .WithMany()
            .HasForeignKey(w => w.UserId)
            .IsRequired()
            .OnDelete(DeleteBehavior.Cascade);
    }
}
