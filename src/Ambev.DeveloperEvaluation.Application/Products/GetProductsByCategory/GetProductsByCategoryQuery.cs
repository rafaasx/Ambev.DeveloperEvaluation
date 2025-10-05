using Ambev.DeveloperEvaluation.Application.Products.GetProduct;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProductsByCategory;


public class GetProductsByCategoryQuery : IRequest<IQueryable<GetProductResult>>
{
    public string Category { get; }
    public int Page { get; }
    public int Size { get; }
    public string? Order { get; }

    public GetProductsByCategoryQuery(string category, int page = 1, int size = 10, string? order = null)
    {
        Category = category;
        Page = page;
        Size = size;
        Order = order;
    }
}