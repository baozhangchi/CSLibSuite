using System;
using System.Collections.Generic;
using System.Linq;

namespace Utils.Extensions
{
    public static class EnumerableExtensions
    {
#if NET6_0_OR_GREATER
        public static IEnumerable<T> Distinct<T, TK>(this IEnumerable<T> source, Func<T, TK> selector)
        {
            return source.DistinctBy(selector);
        }

        public static IEnumerable<T> Union<T, TK>(this IEnumerable<T> left, IEnumerable<T> right, Func<T, TK> selector)
        {
            return left.UnionBy(right, selector);
        }
#else
        public static IEnumerable<T> Distinct<T, TK>(this IEnumerable<T> source, Func<T, TK> selector)
        {
            return source.Distinct(new DynamicEqualityComparer<T, TK>(selector));
        }

        public static IEnumerable<T> Union<T, TK>(this IEnumerable<T> left, IEnumerable<T> right, Func<T, TK> selector)
        {
            return left.Union(right, new DynamicEqualityComparer<T, TK>(selector));
        }
#endif
        public static IEnumerable<T> Except<T, TK>(this IEnumerable<T> left, IEnumerable<T> right, Func<T, TK> selector)
        {
            return left.Except(right, new DynamicEqualityComparer<T, TK>(selector));
        }

        public static IEnumerable<T> Intersect<T, TK>(this IEnumerable<T> left, IEnumerable<T> right, Func<T, TK> selector)
        {
            return left.Intersect(right, new DynamicEqualityComparer<T, TK>(selector));
        }

        private class DynamicEqualityComparer<T, TK> : IEqualityComparer<T>
        {
            private readonly Func<T, TK> _selector;

            public DynamicEqualityComparer(Func<T, TK> selector)
            {
                this._selector = selector;
            }

            public bool Equals(T x, T y)
            {
                return EqualityComparer<TK>.Default.Equals(_selector(x), _selector(y));
            }

            public int GetHashCode(T obj)
            {
                return EqualityComparer<TK>.Default.GetHashCode(_selector(obj));
            }
        }
    }
}
