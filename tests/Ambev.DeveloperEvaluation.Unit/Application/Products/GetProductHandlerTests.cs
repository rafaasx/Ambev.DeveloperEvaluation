using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

public class GetProductHandlerTests
{
    [Fact]
    public async Task Handle_ExistingProduct_ReturnsResult()
    {
        // Arrange
        var repository = Substitute.For<IProductRepository>();
        var mapper = Substitute.For<IMapper>();
        var handler = new GetProductCommandHandler(repository, mapper);
        var productId = Guid.NewGuid();
        var command = new GetProductCommand(productId);
        var product = new Product("Cerveja", 5m, "Descrição", "img.jpg", "category", new Rating(4.5, 10));
        repository.GetByIdAsync(productId, Arg.Any<CancellationToken>()).Returns(product);
        var expected = new GetProductResult { Title = product.Title, Price = product.Price };
        mapper.Map<GetProductResult>(product).Returns(expected);

        // Act
        var result = await handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Title.Should().Be(product.Title);
        await repository.Received(1).GetByIdAsync(productId, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_ProductNotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
        var repository = Substitute.For<IProductRepository>();
        var mapper = Substitute.For<IMapper>();
        var handler = new GetProductCommandHandler(repository, mapper);
        var productId = Guid.NewGuid();
        var command = new GetProductCommand(productId);
        repository.GetByIdAsync(productId, Arg.Any<CancellationToken>()).Returns((Product?)null);

        // Act
        Func<Task> act = async () => await handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>();
        await repository.Received(1).GetByIdAsync(productId, Arg.Any<CancellationToken>());
    }
}