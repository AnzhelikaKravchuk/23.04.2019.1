﻿using System;
using System.Collections.Generic;

namespace PseudoEnumerable
{
    public static class EnumerableExtension
    {
        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            IPredicate<TSource> predicate)
        {
            if (predicate == null)
            {
                yield break;
            }

            foreach (var i in source)
            {
                if (predicate.IsMatching(i))
                {
                    yield return i;
                }
            }
        }

        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            ITransformer<TSource, TResult> transformer)
        {
            if (transformer == null)
            {
                yield break;
            }

            foreach (var i in source)
            {
                yield return transformer.Transform(i);
            }
        }

        public static IEnumerable<TSource> SortBy<TSource>(this IEnumerable<TSource> source,
            IComparer<TSource> comparer)
        {
            if (comparer == null)
            {
                throw new ArgumentNullException($"comparer is null{nameof(comparer)}");
            }

            var copyArray = source.ToArray();
            Array.Sort(copyArray, comparer);

            return copyArray;
        }

        public static IEnumerable<TSource> Filter<TSource>(this IEnumerable<TSource> source,
            Predicate<TSource> predicate)
        {
            // Call EnumerableExtension.Filter with interface
            throw new NotImplementedException();
        }

        public static IEnumerable<TResult> Transform<TSource, TResult>(this IEnumerable<TSource> source,
            Converter<TSource, TResult> transformer)
        {
            // Implementation Day 13 Task 1 (ArrayExtension)
            throw new NotImplementedException();
        }

        public static IEnumerable<TSource> SortBy<TSource>(this IEnumerable<TSource> source,
            Comparison<TSource> comparer)
        {
            // Call EnumerableExtension.SortBy with interface
            throw new NotImplementedException();
        }


        private static TElement[] ToArray<TElement>(this IEnumerable<TElement> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException($"{nameof(source)}");
            }

            TElement[] array = null;

            if (source is ICollection<TElement> elements)
            {
                array = new TElement[elements.Count];
                elements.CopyTo(array, 0);
                return array;
            }
            else
            {
                var tempArray = new TElement[source.Count()];
                int i = 0;
                foreach (var element in source)
                {
                    tempArray[i] = element;
                    i++;
                }

                array = new TElement[source.Count()];
                Array.Copy(tempArray, array, tempArray.Length);
                return array;
            }
        }

        private static int Count<TSource>(this IEnumerable<TSource> source)
        {
            if (source is null)
            {
                throw new ArgumentNullException($"{nameof(source)}");
            }

            int count = 0;
            while (source.GetEnumerator().MoveNext())
            {
                count++;
            }

            return count;
        }
    }
}
