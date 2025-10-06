using Ambev.DeveloperEvaluation.Integration.Base;
using Ambev.DeveloperEvaluation.Integration.Fixtures;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale;
using FluentAssertions;
using System.Net;
using System.Net.Http.Json;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Features.Products
{
    public class UpdateSaleIntegrationTests(WebApplicationFixture fixture) : IntegrationTestBase(fixture)
    {
        [Fact]
        public async Task UpdateSale_Should_Return_Ok()
        {
            // Arrange
            var createRequest = new CreateSaleRequest
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

            var createResponse = await PostAsync("/api/sales", createRequest);
            createResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            var createdSale = await createResponse.Content.ReadFromJsonAsync<ApiResponseWithData<CreateSaleResponse>>();
            createdSale.Should().NotBeNull();
            createdSale!.Data.Should().NotBeNull();

            // Atualiza alguns campos da venda
            var updateRequest = new UpdateSaleRequest
            {
                Id = createdSale.Data.Id,
                SaleNumber = createdSale.Data.SaleNumber,
                SaleDate = createdSale.Data.SaleDate,
                CustomerId = createdSale.Data.CustomerId,
                CustomerName = "Updated Customer Name",
                BranchId = createdSale.Data.BranchId,
                BranchName = createdSale.Data.BranchName,
                Products = createdSale.Data.Products.Select(p => new UpdateSaleItemRequest
                {
                    ProductId = p.ProductId,
                    ProductName = p.ProductName == "Product A" ? "Updated Product A" : p.ProductName,
                    Quantity = p.Quantity,
                    UnitPrice = p.UnitPrice
                }).ToList()
            };

            // Act
            var updateResponse = await PutAsync($"/api/sales/{updateRequest.Id}", updateRequest);
            updateResponse.StatusCode.Should().Be(HttpStatusCode.OK);

            var updatedSale = await updateResponse.Content.ReadFromJsonAsync<ApiResponseWithData<UpdateSaleResponse>>();
            updatedSale.Should().NotBeNull();
            updatedSale!.Data.Should().NotBeNull();

            // Assert
            updatedSale.Data.CustomerName.Should().Be("Updated Customer Name");
            updatedSale.Data.Products.First(p => p.ProductName.StartsWith("Updated")).ProductName.Should().Be("Updated Product A");
        }
    }
}
