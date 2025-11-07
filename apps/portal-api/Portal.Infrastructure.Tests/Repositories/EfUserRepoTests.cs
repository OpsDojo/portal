// <copyright file="EfUserRepoTests.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Infrastructure.Tests.Repositories;

using Microsoft.EntityFrameworkCore;
using Portal.Domain.Entities;
using Portal.Infrastructure.EF;
using Portal.Infrastructure.EF.Repositories;

/// <summary>
/// Tests for the <see cref="EfUserRepo"/> class.
/// </summary>
public class EfUserRepoTests
{
    [Fact]
    public async Task CreateAsync_WithUser_AddsToDatabase()
    {
        // Arrange
        await using var dbContext = GetInMemDb();
        var sut = new EfUserRepo(dbContext);
        var user = new User("hi", "there", Guid.NewGuid());

        // Act
        await sut.CreateAsync(user);

        // Assert
        var retrievedUser = await dbContext.Users.FindAsync(user.Id);
        retrievedUser.ShouldNotBeNull();
        retrievedUser.DisplayName.ShouldBe("there");
    }

    [Fact]
    public async Task GetByClaim_UserExists_ReturnsUser()
    {
        // Arrange
        await using var dbContext = GetInMemDb();
        var sut = new EfUserRepo(dbContext);
        var user = new User("why", "yes", Guid.NewGuid());
        dbContext.Users.Add(user);
        await dbContext.SaveChangesAsync();

        // Act
        var retrievedUser = await sut.GetBySubjectClaim("why");

        // Assert
        retrievedUser.ShouldNotBeNull();
        retrievedUser.DisplayName.ShouldBe("yes");
    }

    [Fact]
    public async Task GetByClaim_UserDoesNotExist_ReturnsNull()
    {
        // Arrange
        await using var dbContext = GetInMemDb();
        var sut = new EfUserRepo(dbContext);

        // Act
        var retrievedUser = await sut.GetBySubjectClaim("hello");

        // Assert
        retrievedUser.ShouldBeNull();
    }

    private static PortalDbContext GetInMemDb(string? dbName = null)
    {
        var options = new DbContextOptionsBuilder<PortalDbContext>()
            .UseInMemoryDatabase(databaseName: dbName ?? Guid.NewGuid().ToString())
            .Options;

        return new PortalDbContext(options);
    }
}
