using Ambev.DeveloperEvaluation.Integration.Base;
using Ambev.DeveloperEvaluation.Integration.Fixtures;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using FluentAssertions;
using System.Net;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Features.Sales
{
    public class CreateSaleIntegrationTests(WebApplicationFixture fixture) : IntegrationTestBase(fixture)
    {

        [Fact]
        public async Task CreateSale_Should_Return_Created()
        {
            var request = new CreateSaleRequest
            {
                SaleNumber = "123456",
                SaleDate = DateTime.UtcNow,
                CustomerId = Guid.NewGuid(),
                CustomerName = "Rafael Test",
                BranchId = Guid.NewGuid(),
                BranchName = "Main Branch",
                IsCancelled = false,
                Products = new List<CreateSaleItemRequest>
                {
                    new CreateSaleItemRequest
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = "Product A",
                        Quantity = 2,
                        UnitPrice = 10.5m
                    },
                    new CreateSaleItemRequest
                    {
                        ProductId = Guid.NewGuid(),
                        ProductName = "Product B",
                        Quantity = 1,
                        UnitPrice = 20.0m
                    }
                }
            };

            var response = await PostAsync("/api/sales", request);
            response.StatusCode.Should().Be(HttpStatusCode.Created);
        }
    }
}