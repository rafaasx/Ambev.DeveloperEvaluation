using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategory;


public class GetProductsByCategoryQueryHandler : IRequestHandler<GetProductsByCategoryQuery, IQueryable<GetProductResult>>
{
    private readonly IProductRepository _repository;
    private readonly IMapper _mapper;

    public GetProductsByCategoryQueryHandler(IProductRepository repository, IMapper mapper)
    {
        _repository = repository;
        this._mapper = mapper;
    }

    public async Task<IQueryable<GetProductResult>> Handle(GetProductsByCategoryQuery request, CancellationToken cancellationToken)
    {
        var products = _repository.GetProductsByCategory(request.Category, request.Page, request.Size, request.Order);
        return products.ProjectTo<GetProductResult>(_mapper.ConfigurationProvider);

    }
}
