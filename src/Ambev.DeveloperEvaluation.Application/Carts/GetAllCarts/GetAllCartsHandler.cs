using Ambev.DeveloperEvaluation.Application.Pagination;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Carts.GetAllCarts;

public class GetAllCartsHandler : IRequestHandler<PaginatedQuery<GetAllCartsResult>, IQueryable<GetAllCartsResult>>
{
    private readonly ICartRepository _repository;
    private readonly IMapper _mapper;

    public GetAllCartsHandler(ICartRepository repository, IMapper mapper)
    {
        _repository = repository;
        _mapper = mapper;
    }

    public async Task<IQueryable<GetAllCartsResult>> Handle(PaginatedQuery<GetAllCartsResult> request, CancellationToken cancellationToken)
    {
        var carts = _repository.GetAllPaginated(
            request.PageNumber,
            request.PageSize,
            request.Order
        );

        return carts.ProjectTo<GetAllCartsResult>(_mapper.ConfigurationProvider);
    }
}