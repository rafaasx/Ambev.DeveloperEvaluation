using Ambev.DeveloperEvaluation.Application.Pagination;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.GetAllSales;

public class GetAllSalesHandler(ISaleRepository _repository, IMapper _mapper) : IRequestHandler<PaginatedQuery<GetAllSalesResult>, IQueryable<GetAllSalesResult>>
{
    public async Task<IQueryable<GetAllSalesResult>> Handle(PaginatedQuery<GetAllSalesResult> request, CancellationToken cancellationToken)
    {
        var sales = _repository
            .GetAllAsync(
        request.PageNumber,
        request.PageSize,
                request.Order
            );

        return sales.ProjectTo<GetAllSalesResult>(_mapper.ConfigurationProvider);

    }
}