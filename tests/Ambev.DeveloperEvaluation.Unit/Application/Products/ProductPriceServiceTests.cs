using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

public class ProductPriceServiceTests
{
    [Fact]
    public async Task GetPriceAsync_ProductExists_ReturnsPrice()
    {
        // Arrange
        var repository = Substitute.For<IProductRepository>();
        var productId = Guid.NewGuid();
        var product = new Product("Title", 10m, "Description", "image.jpg", "1", new Rating(4.5, 10));
        repository.GetByIdAsync(productId, Arg.Any<CancellationToken>()).Returns(product);
        var service = new ProductPriceService(repository);

        // Act
        var price = await service.GetPriceAsync(productId, CancellationToken.None);

        // Assert
        price.Should().Be(product.Price);
        await repository.Received(1).GetByIdAsync(productId, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task GetPriceAsync_ProductNotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
        var repository = Substitute.For<IProductRepository>();
        var productId = Guid.NewGuid();
        repository.GetByIdAsync(productId, Arg.Any<CancellationToken>()).Returns((Product?)null);
        var service = new ProductPriceService(repository);

        // Act
        Func<Task> act = async () => await service.GetPriceAsync(productId, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>();
        await repository.Received(1).GetByIdAsync(productId, Arg.Any<CancellationToken>());
    }
}
