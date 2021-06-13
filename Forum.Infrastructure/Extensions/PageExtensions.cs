using Forum.Domain.SeedWork;

using Microsoft.EntityFrameworkCore;

using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Forum.Infrastructure.Extensions
{
    public static class PageExtensions
    {
        public static async Task<Page<T>> ToPageAsync<T>(
            this IQueryable<T> query, 
            Pagination pagination, 
            CancellationToken cancellationToken = default)
        {
            var count = await query.CountAsync(cancellationToken);

            var totalPages = (int)Math.Ceiling((double)count / pagination.PageSize);

    
            var items = await query
                .Skip(pagination.PageIndex * pagination.PageSize)
                .Take(pagination.PageSize)
                .ToListAsync(cancellationToken);

            return new Page<T>(totalPages, items);
        }
    }
}
