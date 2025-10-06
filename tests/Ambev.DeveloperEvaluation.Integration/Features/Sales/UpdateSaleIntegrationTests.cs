using Ambev.DeveloperEvaluation.Integration.Base;
using Ambev.DeveloperEvaluation.Integration.Fixtures;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Features.Sales
{
    public class UpdateSaleIntegrationTests(WebApplicationFixture fixture) : IntegrationTestBase(fixture)
    {
        [Fact]
        public async Task UpdateSale_Should_Return_Ok()
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
            createdSale.Data.CustomerName = "Updated Customer Name";
            createdSale.Data.Products[0].ProductName = "Updated Product A";
            //Act
            response = await PutAsync($"/api/sales/{createdSale.Data.Id}", createdSale.Data);
            var updatedSale = await response.Content.ReadFromJsonAsync<ApiResponseWithData<UpdateSaleResponse>>();

            //Assert
            response.StatusCode.Should().Be(HttpStatusCode.OK);
            updatedSale.Data.CustomerName.Should().Be(createdSale.Data.CustomerName);
            updatedSale.Data.Products[0].ProductName.Should().Be(createdSale.Data.Products[0].ProductName);
        }
    }
}
