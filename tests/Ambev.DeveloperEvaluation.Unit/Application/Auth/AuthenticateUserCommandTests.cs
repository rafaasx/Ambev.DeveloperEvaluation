using Ambev.DeveloperEvaluation.Application.Auth.AuthenticateUser;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Auth;

public class AuthenticateUserCommandTests
{
    [Fact]
    public void Constructor_ShouldSetPropertiesCorrectly()
    {
        var command = new AuthenticateUserCommand
        {
            Email = "admin@gmail.com",
            Password = "123456"
        };
        Assert.Equal("admin@gmail.com", command.Email);
        Assert.Equal("123456", command.Password);
    }
}
