using Ambev.DeveloperEvaluation.Application.Pagination;
using Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.WebApi.Common;
using AutoMapper;
using Bogus;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Products;

public class GetAllProductsHandlerTests
{
    private readonly IProductRepository _productRepository = Substitute.For<IProductRepository>();
    private readonly IMapper _mapper;
    private readonly GetAllProductsHandler _handler;
    private readonly Faker _faker = new Faker("pt_BR");

    public GetAllProductsHandlerTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new GetAllProductsProfile());
        });
        _mapper = config.CreateMapper();
        _handler = new GetAllProductsHandler(_productRepository, _mapper);
    }

    [Fact]
    public async Task Handle_Should_ReturnPaginatedProducts()
    {
        var paginationQuery = new PaginatedQuery<GetAllProductsQuery, GetAllProductsResult>(
            pageNumber: 1,
            pageSize: 10,
            order: "Title",
            filter: new GetAllProductsQuery { Name = "Sample Product" }
        );

        var products = GenerateFakePaginatedProducts();
        var mappedProducts = products.Select(p => GenerateFakeListProductResponse(p));

        _productRepository.GetAllPaginated(
            Arg.Any<int>(),
            Arg.Any<int>(),
            Arg.Any<string>()
        ).Returns(products);

        var result = await _handler.Handle(paginationQuery, CancellationToken.None);
        var paginatedResult = PaginatedList<GetAllProductsResult>.Create(result, paginationQuery.PageNumber, paginationQuery.PageSize);

        result.Should().NotBeNull();
        paginationQuery.PageSize.Should().Be(paginatedResult.TotalCount);
        paginationQuery.PageNumber.Should().Be(paginatedResult.CurrentPage);
    }

    private IQueryable<Product> GenerateFakePaginatedProducts()
    {
        var products = new List<Product>();

        for (int i = 0; i < 10; i++)
        {
            var newProduct = new Product(
                title: _faker.Commerce.ProductName(),
                price: _faker.Random.Decimal(10, 1000),
                description: _faker.Commerce.ProductDescription(),
                image: _faker.Image.PicsumUrl(),
                category: _faker.Commerce.Categories(1).First(),
                rating: new Rating(rate: _faker.Random.Double(1, 5), count: _faker.Random.Int(1, 100)
                )
            );
            products.Add(newProduct);
        }

        return products.AsQueryable();
    }

    private static GetAllProductsResult GenerateFakeListProductResponse(Product product)
    {
        return new GetAllProductsResult
        {
            Title = product.Title,
            Price = product.Price,
            Description = product.Description,
            Image = product.Image,
            Category = product.Category,
            Rating = new GetlAllProductsRatingResponse
            {
                Rate = product.Rating.Rate,
                Count = product.Rating.Count
            }
        };
    }
}
