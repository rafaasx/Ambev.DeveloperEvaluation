using Ambev.DeveloperEvaluation.Integration.Base;
using Ambev.DeveloperEvaluation.Integration.Fixtures;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreatProduct;
using FluentAssertions;
using System.Net;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Features.Products
{
    public class CreateProductIntegrationTests(WebApplicationFixture fixture) : IntegrationTestBase(fixture)
    {
        [Fact]
        public async Task CreateProduct_Should_Return_Created()
        {
            var request = new CreateProductRequest
            {
                Title = "Cerveja Pilsen 600ml",
                Price = 8.90m,
                Description = "Cerveja Pilsen gelada e refrescante",
                Image = "https://cdn.ambev.com/pilsen.png",
                Category = "Bebidas",
                Rating = new CreateProductRatingRequest
                {
                    Rate = 4.5,
                    Count = 120
                }
            };

            var response = await PostAsync("/api/products", request);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }
    }
}
