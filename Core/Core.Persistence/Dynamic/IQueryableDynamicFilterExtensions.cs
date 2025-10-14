using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Text;
using System.Threading.Tasks;

namespace Core.Persistence.Dynamic;

public static class IQueryableDynamicFilterExtensions
{
    private static readonly string[] _orders = { "asc", "desc" };

    private static readonly IDictionary<string, string> _operators = new Dictionary<string, string>
    {
        { "startswith", "StartsWith" },
        { "endswith", "EndsWith" },
        { "contains", "Contains" },
        { "doesnotcontain", "Contains" }
    };

    public static IQueryable<T> ToDynamic<T>(this IQueryable<T> query, DynamicQuery dynamicQuery)
    {
        if (dynamicQuery.Filter is not null)
            query = Filter(query, dynamicQuery.Filter);
        if (dynamicQuery.Sort is not null )
            query = Sort(query, dynamicQuery.Sort);
        return query;
    }
    private static IQueryable<T> Filter<T>(IQueryable<T> queryable, Filter filter)
    {
        string where = Transform(filter);

        if (!string.IsNullOrEmpty(where))
            queryable = queryable.Where(where,filter.Value);

        return queryable;
    }

    private static IQueryable<T> Sort<T>(IQueryable<T> queryable, Sort sort)
    {
        if (string.IsNullOrEmpty(sort.Field))
            throw new ArgumentException("Invalid Field");

        if (string.IsNullOrEmpty(sort.Dir) || !_orders.Contains(sort.Dir))
            throw new ArgumentException("Invalid Order Type");

        string ordering = $"{sort.Field} {sort.Dir}";

        return queryable.OrderBy(ordering);
    }

    public static string Transform(Filter filter)
    {
        if (string.IsNullOrEmpty(filter.Field))
            throw new ArgumentException("Invalid Field");
        if (string.IsNullOrEmpty(filter.Operator) || !_operators.ContainsKey(filter.Operator))
            throw new ArgumentException("Invalid Operator");

        string comparison = _operators[filter.Operator];
        StringBuilder where = new();

        if (!string.IsNullOrEmpty(filter.Value))
        {
            if (filter.Operator == "doesnotcontain")
                where.Append($"!{filter.Field}.{comparison}(@0)");
            else
                where.Append($"{filter.Field}.{comparison}(@0)");

        }

        return where.ToString();
    }



}
