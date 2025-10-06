using System.Linq.Dynamic.Core;

namespace Ambev.DeveloperEvaluation.ORM.Extensions;

public static class QueryableExtensions
{
    public static IQueryable<T> SortBy<T>(this IQueryable<T> query, string? order)
    {
        if (string.IsNullOrWhiteSpace(order))
            return query;
        var orders = order.Split(',');
        foreach (var orderBy in orders)
        {
            var parts = orderBy.Trim().Split(' ');
            if (parts.Length == 2)
            {
                var property = parts[0];
                var direction = parts[1].Equals("desc", StringComparison.CurrentCultureIgnoreCase) ? "descending" : "ascending";
                query = query.OrderBy($"{property} {direction}");
            }
        }
        return query;
    }
}
