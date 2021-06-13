using System;
using System.Collections.Generic;

namespace Forum.Domain.SeedWork
{
    public class Pagination : ValueObject
    {
        public Pagination(int pageNumber, int pageSize)
        {
            if (pageNumber < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageNumber), pageNumber,
                    "Page number can't be less than 1");
            }

            PageNumber = pageNumber;

            if (pageSize < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(pageSize), pageSize,
                    "Page size can't be less than 1");
            }

            PageSize = pageSize;
        }

        public int PageNumber { get; }
        public int PageSize { get; }

        public int PageIndex => PageNumber - 1;

        public override IEnumerable<object> GetAtomicValues()
        {
            return new object[] { PageNumber, PageSize };
        }

        public Pagination Resize(int newSize) => new Pagination(PageNumber, newSize);
        public Pagination Previous => new Pagination(PageNumber - 1, PageSize);
        public Pagination Next => new Pagination(PageNumber + 1, PageSize);
    }
}
