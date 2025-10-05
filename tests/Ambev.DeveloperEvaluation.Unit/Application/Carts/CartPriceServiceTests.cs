using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

public class CartPriceServiceTests
{
    [Fact]
    public async Task GetTotalPriceAsync_CartExists_ReturnsTotal()
    {
        // Arrange
        var repository = Substitute.For<ICartRepository>();
        var cartId = Guid.NewGuid();
        var cart = new Cart(Guid.NewGuid(), DateTime.Now);
        cart.UpdateProductQuantity(Guid.NewGuid(), 3, 10m, "productTitle1"); // 30
        cart.UpdateProductQuantity(Guid.NewGuid(), 5, 10m, "productTitle2"); // 50 with discount 10% = 45
        repository.GetByIdAsync(cartId, Arg.Any<CancellationToken>()).Returns(cart);
        var service = new CartPriceService(repository);

        // Act
        var total = await service.GetTotalPriceAsync(cartId);

        // Assert
        total.Should().Be(cart.TotalPrice);
        await repository.Received(1).GetByIdAsync(cartId, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task GetTotalPriceAsync_CartNotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
        var repository = Substitute.For<ICartRepository>();
        var cartId = Guid.NewGuid();
        repository.GetByIdAsync(cartId, Arg.Any<CancellationToken>()).Returns((Cart?)null);
        var service = new CartPriceService(repository);

        // Act
        Func<Task> act = async () => await service.GetTotalPriceAsync(cartId);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>();
        await repository.Received(1).GetByIdAsync(cartId, Arg.Any<CancellationToken>());
    }
}