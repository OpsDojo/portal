// <copyright file="UserServiceTests.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Application.Tests.System;

using Portal.Application.System;
using Portal.Domain.Entities;

/// <summary>
/// Tests for the <see cref="UserService"/> class.
/// </summary>
public class UserServiceTests
{
    [Fact]
    public async Task AddIfMissing_UserIsMissing_AddsUser()
    {
        // Arrange
        const string subject = "test-sub";
        const string name = "test-name";
        var mockRepo = new Mock<IUserRepository>();
        var sut = new UserService(mockRepo.Object);

        // Act
        _ = await sut.AddIfMissing(subject, name);

        // Assert
        mockRepo.Verify(
            m => m.CreateAsync(
                It.Is<User>(u => u.Subject == subject && u.DisplayName == name),
                It.IsAny<CancellationToken>()),
            Times.Once);
    }

    [Fact]
    public async Task AddIfMissing_UserExists_DoesNotAdd()
    {
        // Arrange
        const string subject = "test-sub";
        const string name = "test-name";
        var mockRepo = new Mock<IUserRepository>();
        var sut = new UserService(mockRepo.Object);
        mockRepo.Setup(m => m.GetBySubjectClaim(subject, It.IsAny<CancellationToken>()))
            .ReturnsAsync(new User(subject, name));

        // Act
        _ = await sut.AddIfMissing(subject, name);

        // Assert
        mockRepo.Verify(
            m => m.CreateAsync(It.IsAny<User>(), It.IsAny<CancellationToken>()),
            Times.Never);
    }
}
