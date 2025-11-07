// <copyright file="EfWeightLogRepoTests.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Infrastructure.Tests.Repositories;

using Microsoft.EntityFrameworkCore;
using Portal.Domain.Entities;
using Portal.Infrastructure.EF;
using Portal.Infrastructure.EF.Repositories;

/// <summary>
/// Tests for the <see cref="EfWeightLogRepo"/> class.
/// </summary>
public class EfWeightLogRepoTests
{
    [Fact]
    public async Task GetPageAsync_WithWeightLogs_ReturnsExpected()
    {
        // Arrange
        await using var dbContext = GetInMemDb();
        var sut = new EfWeightLogRepo(dbContext);
        var userId = Guid.NewGuid();
        dbContext.WeightLogs.AddRange(
            Enumerable.Range(1, 25)
                .Select(n => new WeightLog(DateOnly.FromDateTime(DateTime.UtcNow.AddDays(-n)), new(70 + n), userId)));
        await dbContext.SaveChangesAsync();

        // Act
        var (items, total) = await sut.GetPageAsync(userId, pageNumber: 2, pageSize: 10);

        // Assert
        total.ShouldBe(25);
        items.Count.ShouldBe(10);
        items[0].Date.ShouldBeGreaterThan(items[^1].Date);
    }

    private static PortalDbContext GetInMemDb(string? dbName = null)
    {
        var options = new DbContextOptionsBuilder<PortalDbContext>()
            .UseInMemoryDatabase(databaseName: dbName ?? Guid.NewGuid().ToString())
            .Options;

        return new PortalDbContext(options);
    }
}
