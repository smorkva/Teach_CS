using System;
using System.Collections.Generic;

namespace Extension
{
    public static class CollencionExtention
    {
        static TSource _sumByPredicate<TSource>(this IEnumerable<TSource> collection, Func<TSource, bool> predicate)
        {
            var result = default(TSource);
            foreach (var item in collection)
            {
                if (predicate(item))
                {
                    result += (dynamic)item;
                }
            }

            return result;
        }
        public static int SumByPredicate(this IEnumerable<int> collection, Func<int, bool> predicate)
        {
            return _sumByPredicate(collection, predicate);
        }
        public static long SumByPredicate(this IEnumerable<long> collection, Func<long, bool> predicate)
        {
            return _sumByPredicate(collection, predicate);
        }
        public static double SumByPredicate(this IEnumerable<double> collection, Func<double, bool> predicate)
        {
            return _sumByPredicate(collection, predicate);
        }
    }
}
