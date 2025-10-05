using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Pagination;

public class PaginatedQuery<TFilter, TResponse> : IRequest<IQueryable<TResponse>>
{
    public int PageNumber { get; }
    public int PageSize { get; }
    public string? Order { get; }
    public TFilter? Filter { get; }

    public PaginatedQuery() { }

    public PaginatedQuery(int pageNumber, int pageSize, string? order, TFilter? filter)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        Order = order;
        Filter = filter;
    }
}

public class PaginatedQuery<TResponse> : IRequest<IQueryable<TResponse>>
{
    public int PageNumber { get; }
    public int PageSize { get; }
    public string? Order { get; }

    public PaginatedQuery() { }

    public PaginatedQuery(int pageNumber, int pageSize, string? order)
    {
        PageNumber = pageNumber;
        PageSize = pageSize;
        Order = order;
    }
}
