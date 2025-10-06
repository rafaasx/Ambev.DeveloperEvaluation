using Ambev.DeveloperEvaluation.Integration.Base;
using Ambev.DeveloperEvaluation.Integration.Fixtures;
using FluentAssertions;
using System.Net;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Features.Sales
{
    public class GetAllSaleIntegrationTests(WebApplicationFixture fixture) : IntegrationTestBase(fixture)
    {
        [Fact]
        public async Task GetAllSales_Should_Return_Success()
        {
            var response = await GetAsync("/api/sales");
            response.StatusCode.Should().Be(HttpStatusCode.OK);
        }
    }
}
