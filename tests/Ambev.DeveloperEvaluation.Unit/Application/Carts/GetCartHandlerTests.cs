using Ambev.DeveloperEvaluation.Application.Carts.GetCart;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

public class GetCartHandlerTests
{
    private readonly ICartRepository _cartRepository;
    private readonly IMapper _mapper;
    private readonly GetCartCommandHandler _handler;

    public GetCartHandlerTests()
    {
        _cartRepository = Substitute.For<ICartRepository>();
        _mapper = Substitute.For<IMapper>();
        _handler = new GetCartCommandHandler(_cartRepository, _mapper);
    }

    [Fact]
    public async Task Handle_ExistingCart_ReturnsResult()
    {
        // Arrange
        var cartId = Guid.NewGuid();
        var command = new GetCartCommand(cartId);

        var cart = new Cart(Guid.NewGuid(), DateTime.Now);
        cart.UpdateProductQuantity(Guid.NewGuid(), 2, 10m, "productTitle1");
        _cartRepository.GetByIdAsync(cartId, Arg.Any<CancellationToken>()).Returns(cart);

        var expected = new GetCartResult
        {
            Id = cart.Id,
            UserId = cart.UserId,
            Date = cart.Date,
            TotalPrice = cart.TotalPrice,
            Products = cart.Products
                .Select(p => new GetCartItemResult
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity,
                    UnitPrice = p.UnitPrice,
                    TotalPrice = p.TotalPrice,
                    TotalPriceWithDiscount = p.TotalPriceWithDiscount,
                    Discount = p.Discount
                }).ToList()
        };
        _mapper.Map<GetCartResult>(cart).Returns(expected);

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        result.Should().NotBeNull();
        result.Id.Should().Be(cart.Id);
        result.TotalPrice.Should().Be(cart.TotalPrice);
        await _cartRepository.Received(1).GetByIdAsync(cartId, Arg.Any<CancellationToken>());
    }

    [Fact]
    public async Task Handle_CartNotFound_ThrowsKeyNotFoundException()
    {
        // Arrange
        var cartId = Guid.NewGuid();
        var command = new GetCartCommand(cartId);
        _cartRepository.GetByIdAsync(cartId, Arg.Any<CancellationToken>()).Returns((Cart?)null);

        // Act
        Func<Task> act = async () => await _handler.Handle(command, CancellationToken.None);

        // Assert
        await act.Should().ThrowAsync<KeyNotFoundException>();
        await _cartRepository.Received(1).GetByIdAsync(cartId, Arg.Any<CancellationToken>());
    }
}
