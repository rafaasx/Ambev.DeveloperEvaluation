using Ambev.DeveloperEvaluation.Integration.Base;
using Ambev.DeveloperEvaluation.Integration.Fixtures;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreatProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProduct;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Features.Products
{
    public class GetProductIntegrationTests(WebApplicationFixture fixture) : IntegrationTestBase(fixture)
    {
        [Fact]
        public async Task GetProduct_Should_Return_Ok()
        {
            // Arrange — cria um produto antes de buscar
            var request = new CreateProductRequest
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

            // Cria o produto
            var response = await PostAsync("/api/products", request);
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var createdProduct = await response.Content.ReadFromJsonAsync<ApiResponseWithData<CreateProductResponse>>();
            createdProduct.Should().NotBeNull();
            createdProduct!.Data.Should().NotBeNull();

            // Act — busca o produto criado
            response = await GetAsync($"/api/products/{createdProduct.Data.Id}");
            response.StatusCode.Should().Be(HttpStatusCode.OK);

            var getProduct = await response.Content.ReadFromJsonAsync<ApiResponseWithData<GetProductResponse>>();

            // Assert — valida os dados retornados
            getProduct.Should().NotBeNull();
            getProduct!.Data.Should().NotBeNull();
            getProduct.Data.Id.Should().Be(createdProduct.Data.Id);
            getProduct.Data.Title.Should().Be(request.Title);
            getProduct.Data.Price.Should().Be(request.Price);
        }
    }
}
