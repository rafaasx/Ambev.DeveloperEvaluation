using Ambev.DeveloperEvaluation.Application.Pagination;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetAllProducts;

public class GetAllProductsHandler : IRequestHandler<PaginatedQuery<GetAllProductsQuery, GetAllProductsResult>, IQueryable<GetAllProductsResult>>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;
    public GetAllProductsHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IQueryable<GetAllProductsResult>> Handle(PaginatedQuery<GetAllProductsQuery, GetAllProductsResult> request, CancellationToken cancellationToken)
    {
        var products = _repository.GetAllPaginated(
            request.PageNumber,
            request.PageSize,
            request.Order
        );
        return products.ProjectTo<GetAllProductsResult>(_mapper.ConfigurationProvider);

    }
}
