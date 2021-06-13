using System;
using System.Linq;
using System.Collections.Generic;

namespace Forum.Domain.SeedWork
{
    public class Page<T>
    {
        public Page(int totalPages, IEnumerable<T> items)
        {
            if (totalPages < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(totalPages), totalPages, 
                    "Total can't be less than 0");
            }

            TotalPages = totalPages;
            Items = new List<T>(items);
        }

        public int TotalPages { get; }
        public IReadOnlyCollection<T> Items { get; }

        public Page<TOther> Map<TOther>(Func<T, TOther> mapFunc)
        {
            return new Page<TOther>(TotalPages, Items.Select(mapFunc));
        }
    }
}
