using Ambev.DeveloperEvaluation.Application.Carts.CreateCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services.Interfaces;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

public class CreateCartHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly IProductPriceService _productPriceService;
    private readonly IMapper _mapper;
    private readonly CreateCartHandler _handler;

    public CreateCartHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _productPriceService = Substitute.For<IProductPriceService>();
        _mapper = Substitute.For<IMapper>();
        _handler = new CreateCartHandler(_cartRepository, _productPriceService, _mapper);
    }

    [Fact]
    public async Task Handle_ValidCommand_ReturnsCreateCartResult()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var products = new List<CreateCartItemCommand>
        {
            new CreateCartItemCommand{ProductId = Guid.NewGuid(), Quantity = 2 },
            new CreateCartItemCommand{ ProductId = Guid.NewGuid(), Quantity = 5 }
        };
        var command = new CreateCartCommand { UserId = userId, Date = DateTime.UtcNow, Products = products };

        foreach (var item in products)
            _productPriceService.GetPriceAsync(item.ProductId, Arg.Any<CancellationToken>()).Returns(10m);

        var cart = new Cart(userId, DateTime.Now);
        foreach (var item in products)
        {
            cart.UpdateProduct(item.ProductId, item.Quantity, 10m, "productTitle1");
        }
        _cartRepository.CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>())
            .Returns(cart);

        var expected = new CreateCartResult
        {
            Id = cart.Id
        };
        _mapper.Map<CreateCartResult>(cart).Returns(expected);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(cart.Id);
        await _productPriceService.Received(products.Count).GetPriceAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>());
        await _cartRepository.Received(1).CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_InvalidProductQuantity_ThrowsException()
    {
        // Arrange
        var userId = Guid.NewGuid();
        var products = new List<CreateCartItemCommand>
        {
            new CreateCartItemCommand{ ProductId = Guid.NewGuid(), Quantity = 0 }
        };
        var command = new CreateCartCommand { UserId = userId, Date = DateTime.UtcNow, Products = products };
        _productPriceService.GetPriceAsync(Arg.Any<Guid>(), Arg.Any<CancellationToken>()).Returns(10m);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<ArgumentOutOfRangeException>();
        await _cartRepository.DidNotReceive().CreateAsync(Arg.Any<Cart>(), Arg.Any<CancellationToken>());
    }
}
