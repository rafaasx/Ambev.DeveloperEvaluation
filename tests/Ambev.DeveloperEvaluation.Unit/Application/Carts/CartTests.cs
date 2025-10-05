using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentAssertions;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

public class CartTests
{
    [Fact]
    public void UpdateItem_NoDiscountForLowQuantity()
    {
        // Arrange
        var productId = Guid.NewGuid();
        var cart = new Cart(Guid.NewGuid(), DateTime.Now);

        // Act
        cart.UpdateProductQuantity(productId, 2, 10m, "productTitle1"); // 2 unidades de 10 reais
        var item = cart.Products.Single();

        // Assert
        item.Quantity.Should().Be(2);
        item.UnitPrice.Should().Be(10m);
        item.TotalPrice.Should().Be(20m);
        item.Discount.Should().Be(0m);
        item.TotalPriceWithDiscount.Should().Be(20m);
    }

    [Fact]
    public void UpdateItem_ApplyTenPercentDiscount()
    {
        // Arrange
        var cart = new Cart(Guid.NewGuid(), DateTime.Now);
        var productId = Guid.NewGuid();

        // Act
        cart.UpdateProductQuantity(productId, 5, 10m, "productTitle1"); // 5 unidades
        var item = cart.Products.Single();

        // Assert
        item.Discount.Should().Be(0.10m);
        item.TotalPriceWithDiscount.Should().BeApproximately(45m, 0.0001m);
    }

    [Fact]
    public void UpdateItem_ApplyTwentyPercentDiscount()
    {
        // Arrange
        var cart = new Cart(Guid.NewGuid(), DateTime.Now);
        var productId = Guid.NewGuid();

        // Act
        cart.UpdateProductQuantity(productId, 12, 10m, "productTitle1");
        var item = cart.Products.Single();

        // Assert
        item.Discount.Should().Be(0.20m);
        item.TotalPriceWithDiscount.Should().BeApproximately(96m, 0.0001m); // 12*10=120, -20% = 96
    }

    [Theory]
    [InlineData(0)]
    [InlineData(21)]
    public void UpdateItem_InvalidQuantity_ThrowsException(int quantity)
    {
        // Arrange
        var cart = new Cart(Guid.NewGuid(), DateTime.Now);
        var productId = Guid.NewGuid();

        // Act
        Action act = () => cart.UpdateProductQuantity(productId, quantity, 10m, "productTitle1");

        // Assert
        act.Should().Throw<ArgumentOutOfRangeException>();
    }

    [Fact]
    public void UpdateTotal_SumsDiscountedPrices()
    {
        // Arrange
        var cart = new Cart(Guid.NewGuid(), DateTime.Now);

        // Act
        cart.UpdateProductQuantity(Guid.NewGuid(), 2, 10m, "productTitle1"); // 20 sem desconto
        cart.UpdateProductQuantity(Guid.NewGuid(), 5, 10m, "productTitle1"); // 50 com 10% desconto = 45

        // Assert
        cart.TotalPrice.Should().BeApproximately(65m, 0.0001m);
    }
}