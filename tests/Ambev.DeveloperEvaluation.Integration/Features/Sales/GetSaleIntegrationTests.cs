using Ambev.DeveloperEvaluation.Integration.Base;
using Ambev.DeveloperEvaluation.Integration.Fixtures;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSale;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Features.Sales
{
    public class GetSaleIntegrationTests(WebApplicationFixture fixture) : IntegrationTestBase(fixture)
    {
        [Fact]
        public async Task GetSale_Should_Return_Ok()
        {
            //Arrange
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
            var createdSale = await response.Content.ReadFromJsonAsync<ApiResponseWithData<CreateSaleResponse>>();
            //Act
            response = await GetAsync($"/api/sales/{createdSale.Data.Id}");
            var getSale = await response.Content.ReadFromJsonAsync<ApiResponseWithData<GetSaleResponse>>();

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            getSale.Data.Id.Should().Be(createdSale.Data.Id);
        }
    }
}
