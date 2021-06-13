using System;
using System.Linq;
using System.Reflection;
using System.Collections.Generic;

namespace Forum.Domain.SeedWork
{
    public abstract class Enumeration<T> : Enumeration<T, int>
        where T : Enumeration<T, int>
    {
        protected Enumeration(int key, string name) : base(key, name)
        {
        }
    }

    public abstract class Enumeration<T, TKey> : ValueObject
        where T : Enumeration<T, TKey>
    {
        private static readonly Lazy<List<T>> LazyValues = new Lazy<List<T>>(() => typeof(T)
                .GetFields(BindingFlags.Public | BindingFlags.Static)
                .Where(f => typeof(T).IsAssignableFrom(f.FieldType))
                .Select(f => (T)f.GetValue(null))
                .ToList(), isThreadSafe: false);

        protected Enumeration(TKey key, string name)
        {
            Key = key;
            Name = name;
        }

        public static List<T> Values
        {
            get
            {
                return new List<T>(LazyValues.Value);
            }
        }

        public static T FromName(string name)
        {
            return Values.Single(v => v.Name.Equals(name));
        }

        public static T FromName(string name, StringComparison stringComparison)
        {
            return Values.Single(v => v.Name.Equals(name, stringComparison));
        }

        public static T FromKey(TKey key)
        {
            return Values.Single(v => v.Key.Equals(key));
        }

        public TKey Key { get; }
        public string Name { get; }

        public override IEnumerable<object> GetAtomicValues()
        {
            return new object[] { Key, Name };
        }
    }
}
