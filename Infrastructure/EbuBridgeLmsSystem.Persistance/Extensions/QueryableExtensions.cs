using EbuBridgeLmsSystem.Domain.Entities.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace EbuBridgeLmsSystem.Persistance.Extensions
{
    public static class QueryableExtensions
    {
        public static async Task<PaginatedResult<T>> PaginateCursorAsync<T, TKey>(
        this IQueryable<T> query,
        string cursor,
        int limit,
        Expression<Func<T, TKey>> keySelector,
        Func<IQueryable<T>, IQueryable<T>>[] includes
        )
        where TKey : IComparable
        {
            
            if (includes != null && includes.Length > 0)
            {
                foreach (var include in includes)
                {
                    query = include(query);
                }
            }

            // Ensure a deterministic order by sorting with the provided key selector.
            query = query.OrderBy(keySelector);

            // If a cursor is provided, convert it to the appropriate type.
            if (!string.IsNullOrEmpty(cursor))
            {
                TKey lastKey;
                if (typeof(TKey) == typeof(Guid))
                {
                    // Use Guid.Parse for GUID types
                    lastKey = (TKey)(object)Guid.Parse(cursor);
                }
                else
                {
                    lastKey = (TKey)Convert.ChangeType(cursor, typeof(TKey));
                }

                // Build a lambda expression: x => keySelector(x) > lastKey
                var parameter = Expression.Parameter(typeof(T), "x");
                var property = Expression.Invoke(keySelector, parameter);
                var condition = Expression.GreaterThan(property, Expression.Constant(lastKey));
                var lambda = Expression.Lambda<Func<T, bool>>(condition, parameter);

                query = query.Where(lambda);
            }

            // Retrieve limit+1 items to determine if there's a next page.
            var items = await query.Take(limit + 1).ToListAsync();
            bool hasMore = items.Count > limit;
            if (hasMore)
            {
                // Remove the extra item used to detect more data.
                items.RemoveAt(limit);
            }

            // The next cursor is the key (GUID) of the last item in the current page.
            string nextCursor = hasMore ? Convert.ToString(keySelector.Compile()(items.Last())) : null;

            return new PaginatedResult<T>
            {
                Data = (IEnumerable<T>)items,
                NextCursor = nextCursor
            };
        }
    }
}
