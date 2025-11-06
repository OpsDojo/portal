// <copyright file="UserContextTests.cs" company="ne1410s">
// Copyright (c) ne1410s. All rights reserved.
// </copyright>

namespace Portal.Application.Tests.System;

using global::System.Security.Claims;
using Microsoft.AspNetCore.Http;
using Portal.Application.System;
using Portal.Domain.Entities;

/// <summary>
/// Tests for the <see cref="UserContext"/> class.
/// </summary>
public class UserContextTests
{
    [Fact]
    public void GetUser_NoHttpContext_ThrowsExpected()
    {
        // Arrange
        var sut = GetUserSut(out var mocks);
        mocks.MockHttpAccessor.Setup(m => m.HttpContext).Returns((HttpContext)null!);

        // Act
        var act = () => sut.GetUserAsync();

        // Assert
        act.ShouldThrow<InvalidOperationException>()
            .Message.ShouldBe("Unable to determine user claims.");
    }

    [Fact]
    public void GetUser_NoSubjectClaim_ThrowsExpected()
    {
        // Arrange
        var sut = GetUserSut(out _);

        // Act
        var act = () => sut.GetUserAsync();

        // Assert
        act.ShouldThrow<InvalidOperationException>()
            .Message.ShouldBe("Unable to determine user claims.");
    }

    [Fact]
    public async Task GetUser_ValidClaims_ReturnsExpected()
    {
        // Arrange
        var sut = GetUserSut(out _, new("sub", "test-sub"), new("name", "test-name"));

        // Act
        var user = await sut.GetUserAsync();

        // Assert
        user.ShouldNotBeNull();
        user.Subject.ShouldBe("test-sub");
        user.DisplayName.ShouldBe("test-name");
    }

    [Fact]
    public async Task GetUser_MultipleCalls_CallsServiceOnce()
    {
        // Arrange
        var sut = GetUserSut(out var mocks, new("sub", "test-sub"), new("name", "test-name"));

        // Act
        _ = await sut.GetUserAsync();
        _ = await sut.GetUserAsync();

        // Assert
        mocks.MockUserService.Verify(
            m => m.AddIfMissing("test-sub", "test-name", It.IsAny<CancellationToken>()), Times.Once);
    }

    private static UserContext GetUserSut(out BagOfMocks mocks, params Claim[] claims)
    {
        var httpAccessor = new Mock<IHttpContextAccessor>();
        var userService = new Mock<IUserService>();
        var claimsIdentity = new ClaimsIdentity(claims);
        var httpContext = new DefaultHttpContext
        {
            User = new([claimsIdentity]),
        };

        httpAccessor.Setup(m => m.HttpContext).Returns(httpContext);
        userService
            .Setup(m => m.AddIfMissing(It.IsAny<string>(), It.IsAny<string?>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync((string subject, string? name, CancellationToken _) => new User(subject, name));

        mocks = new(httpAccessor, userService);
        return new UserContext(httpAccessor.Object, userService.Object);
    }

    private record BagOfMocks(
        Mock<IHttpContextAccessor> MockHttpAccessor,
        Mock<IUserService> MockUserService);
}
