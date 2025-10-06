using Ambev.DeveloperEvaluation.Integration.Base;
using Ambev.DeveloperEvaluation.Integration.Fixtures;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreatProduct;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Features.Products
{
    public class DeleteProductIntegrationTests(WebApplicationFixture fixture) : IntegrationTestBase(fixture)
    {
        [Fact]
        public async Task DeleteProduct_Should_Return_Ok()
        {
            // Criação do produto antes de deletar
            var request = new CreateProductRequest
            {
                Title = "Cerveja IPA 500ml",
                Price = 12.50m,
                Description = "Cerveja artesanal estilo IPA",
                Image = "https://cdn.ambev.com/ipa.png",
                Category = "Bebidas",
                Rating = new CreateProductRatingRequest
                {
                    Rate = 4.7,
                    Count = 85
                }
            };

            var response = await PostAsync("/api/products", request);
            response.StatusCode.Should().Be(HttpStatusCode.Created);

            var createdProduct = await response.Content.ReadFromJsonAsync<ApiResponseWithData<CreateProductResponse>>();
            createdProduct.Should().NotBeNull();
            createdProduct!.Data.Should().NotBeNull();

            response = await DeleteAsync($"/api/products/{createdProduct.Data.Id}");

            var deletedResponse = await response.Content.ReadFromJsonAsync<ApiResponse>();
            deletedResponse.Should().NotBeNull();
            deletedResponse!.Success.Should().BeTrue();
            deletedResponse.Message.Should().Be("Product deleted successfully");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
