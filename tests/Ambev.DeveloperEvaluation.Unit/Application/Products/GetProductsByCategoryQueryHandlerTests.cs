using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategory;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.WebApi.Common;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

public class GetProductsByCategoryQueryHandlerTests
{
    [Fact]
    public async Task Handle_ValidQuery_ReturnsPaginatedResult()
    {
        // Arrange
        var repository = Substitute.For<IProductRepository>();
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new GetProductProfile());
        });
        var _mapper = config.CreateMapper();
        var handler = new GetProductsByCategoryQueryHandler(repository, _mapper);
        var category = "Bebidas";
        var query = new GetProductsByCategoryQuery(category, page: 1, size: 10, order: "Title");

        var expected = new List<Product>
        {
            new Product("Cerveja", 5m, "Descrição", "img.jpg","cat-1", new Rating(4.5, 10)),
            new Product("Refrigerante", 4m, "Descrição", "img.jpg", "cat-1", new Rating(4.0, 8))
        }.AsQueryable();


        repository.GetProductsByCategory(category, 1, 10, "Title").Returns(expected);

        // Act
        var result = await handler.Handle(query, CancellationToken.None);
        var paginatedResult = PaginatedList<GetProductResult>.Create(result, query.Page, query.Size);
        // Assert
        result.Should().NotBeNull();
        result.Should().HaveCount(2);
        paginatedResult.TotalCount.Should().Be(2);
        repository.Received(1).GetProductsByCategory(category, 1, 10, "Title");
    }
}