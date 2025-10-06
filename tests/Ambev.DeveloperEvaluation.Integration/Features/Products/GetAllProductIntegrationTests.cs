using Ambev.DeveloperEvaluation.Integration.Base;
using Ambev.DeveloperEvaluation.Integration.Fixtures;
using FluentAssertions;
using System.Net;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Features.Products
{
    public class GetAllProductIntegrationTests(WebApplicationFixture fixture) : IntegrationTestBase(fixture)
    {
        [Fact]
        public async Task GetAllSales_Should_Return_Success()
        {
            var response = await GetAsync("/api/products");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
