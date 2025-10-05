﻿using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using AutoMapper;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Auth;

public class AuthenticateUserProfileTests
{
    [Fact]
    public void Authenticate_User_Profile_Maps_Correctly()
    {
        // Arrange
        var config = new MapperConfiguration(cfg => cfg.AddProfile<AuthenticateUserProfile>());
        var mapper = config.CreateMapper();

        var user = new User
        {
            Username = "Test User",
            Email = "test@example.com",
            Password = "password",
            Phone = "123-456-7890",
            Role = UserRole.Admin,
            Status = UserStatus.Active,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        // Act
        var result = mapper.Map<AuthenticateUserResult>(user);

        // Assert
        Assert.Equal(user.Role.ToString(), result.Role);
        Assert.Empty(result.Token);
    }
}
