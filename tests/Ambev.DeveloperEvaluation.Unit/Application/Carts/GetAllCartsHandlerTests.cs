using Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;
using Ambev.DeveloperEvaluation.Application.Pagination;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.WebApi.Common;
using AutoMapper;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Carts;

public class GetAllCartsHandlerTests
{
    private readonly ICartRepository _cartRepository = Substitute.For<ICartRepository>();
    private readonly IMapper _mapper;
    private readonly GetAllCartsHandler _handler;

    public GetAllCartsHandlerTests()
    {
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile(new GetAllCartsProfile());
        });
        _mapper = config.CreateMapper();
        _handler = new GetAllCartsHandler(_cartRepository, _mapper);
    }

    [Fact]
    public async Task Handle_Should_ReturnPaginatedCarts()
    {
        var paginationQuery = new PaginatedQuery<GetAllCartsResult>(
            pageNumber: 1,
            pageSize: 10,
            order: "Date"
        );

        var carts = GenerateFakePaginatedCarts();
        var mappedResult = carts.Select(GenerateFakeListCartResponse).AsQueryable();
        _cartRepository.GetAllPaginated(
            Arg.Any<int>(),
            Arg.Any<int>(),
            Arg.Any<string>()
        ).Returns(carts);

        var result = await _handler.Handle(paginationQuery, CancellationToken.None);
        var paginatedResult = PaginatedList<GetAllCartsResult>.Create(result, paginationQuery.PageNumber, paginationQuery.PageSize);
        result.Should().NotBeNull();
        paginationQuery.PageSize.Should().Be(paginatedResult.TotalCount);
        paginationQuery.PageNumber.Should().Be(paginatedResult.CurrentPage);
    }

    private static IQueryable<Cart> GenerateFakePaginatedCarts()
    {
        var carts = new List<Cart>();

        for (int i = 0; i < 10; i++)
        {
            var cart = new Cart(userId: Guid.NewGuid(), DateTime.Now);
            cart.UpdateProduct(productId: Guid.NewGuid(), quantity: 1, 1, "productTitle");
            carts.Add(cart);
        }

        return carts.AsQueryable();
    }

    private static GetAllCartsResult GenerateFakeListCartResponse(Cart cart)
    {
        return
            new GetAllCartsResult
            {
                Id = cart.Id,
                UserId = cart.UserId,
                Date = cart.Date,
                Products = cart.Products.Select(p => new GetAllCartItemsResult
                {
                    ProductId = p.ProductId,
                    Quantity = p.Quantity
                }).ToList()
            };
    }

}
