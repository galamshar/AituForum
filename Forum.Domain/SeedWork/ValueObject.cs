using System.Collections.Generic;
using System.Linq;

namespace Forum.Domain.SeedWork
{
    public abstract class ValueObject
    {
        public abstract IEnumerable<object> GetAtomicValues();

        public override bool Equals(object obj)
        {
            if (obj is ValueObject other)
            {
                return other
                    .GetAtomicValues()
                    .SequenceEqual(GetAtomicValues());
            }

            return false;
        }

        public override int GetHashCode()
        {
            const int HashBase = 37;

            int hash = HashBase;

            foreach (var value in GetAtomicValues())
            {
                hash ^= value is { } ? value.GetHashCode() : 0;
            }

            return hash;
        }
    }
}
