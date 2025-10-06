using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Integration.Base;
using Ambev.DeveloperEvaluation.Integration.Fixtures;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.CreateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Carts.UpdateCart;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreatProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Users.CreateUser;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Features.Carts
{
    public class UpdateCartIntegrationTests(WebApplicationFixture fixture) : IntegrationTestBase(fixture)
    {
        [Fact]
        public async Task UpdateCart_Should_Return_Ok()
        {
            // Arrange
            var createdUserRequest = new CreateUserRequest { Username = "Username", Email = $"email{new Random().Next(1, 9999999)}@dominio.com", Password = "Password@123!", Phone = "47996666666", Role = UserRole.Admin, Status = UserStatus.Active };
            var createduserResponse = await PostAsync("/api/users", createdUserRequest);
            var createdUser = await createduserResponse.Content.ReadFromJsonAsync<ApiResponseWithData<CreateUserResponse>>();

            var createProductRequest = new CreateProductRequest
            {
                Title = "Guaraná 2L",
                Price = 6.99m,
                Description = "Refrigerante Guaraná 2 Litros",
                Image = "https://cdn.ambev.com/guarana.png",
                Category = "Bebidas",
                Rating = new CreateProductRatingRequest
                {
                    Rate = 4.3,
                    Count = 250
                }
            };
            var createdProductResponse = await PostAsync("/api/products", createProductRequest);
            var createdProduct = await createdProductResponse.Content.ReadFromJsonAsync<ApiResponseWithData<CreateProductResponse>>();

            var createRequest = new CreateCartRequest
            {
                UserId = createdUser.Data.Id,
                Date = DateTime.UtcNow,
                Products = new List<CreateCartItemRequest>
                {
                    new CreateCartItemRequest
                    {
                        ProductId = createdProduct.Data.Id,
                        ProductTitle = "Product A",
                        Quantity = 2,
                    }
                }
            };

            var createCartResponse = await PostAsync("/api/carts", createRequest);
            var createdCart = await createCartResponse.Content.ReadFromJsonAsync<ApiResponseWithData<CreateCartResponse>>();

            createdProductResponse.StatusCode.Should().Be(HttpStatusCode.Created);
            createdCart.Should().NotBeNull();
            createdCart!.Data.Should().NotBeNull();

            // Atualiza os dados do carrinho
            var updateRequest = new UpdateCartRequest
            {
                Id = createdCart.Data.Id,
                UserId = createdCart.Data.UserId,
                Date = createdCart.Data.Date,
                Products = createdCart.Data.Products.Select(p => new UpdateCartItemRequest
                {
                    ProductId = p.ProductId,
                    ProductTitle = p.ProductTitle == "Product A" ? "Updated Product A" : p.ProductTitle,
                    Quantity = p.Quantity == 2 ? 5 : p.Quantity,
                    UnitPrice = p.UnitPrice
                }).ToList()
            };

            // Act
            var updateResponse = await PutAsync($"/api/carts/{updateRequest.Id}", updateRequest);
            updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var updatedCart = await updateResponse.Content.ReadFromJsonAsync<ApiResponseWithData<UpdateCartResponse>>();
            updatedCart.Should().NotBeNull();
            updatedCart!.Data.Should().NotBeNull();

            // Assert
            updatedCart.Data.Products.First(p => p.ProductTitle.StartsWith("Updated"))
                .ProductTitle.Should().Be("Updated Product A");

            updatedCart.Data.Products.First(p => p.ProductTitle == "Updated Product A")
                .Quantity.Should().Be(5);

            updatedCart.Data.Products.First().UnitPrice.Should().BeGreaterThan(0);
        }
    }
}
