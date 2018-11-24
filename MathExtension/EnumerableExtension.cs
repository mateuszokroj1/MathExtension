using System;
using System.Collections.Generic;

namespace MathExtension.EnumerableExtension
{
    public static class EnumerableExtension
    {
        public static IEnumerable<T> Sort<T>(this IEnumerable<T> input) where T : IComparable
        {
            if (input == null || input.Count() < 2) return input;
            T[] ret = (T[])input;
            for (uint i = 0; i <= ret.Length; i++)
            {
                for (uint index = 0; index < ret.Length - 1; index++)
                {
                    if (ret[index].CompareTo(ret[index + 1]) > 0)
                    {
                        T first = ret[index], second = ret[index + 1];
                        ret[index] = second;
                        ret[index + 1] = first;
                    }
                }
            }
            return ret;
        }

        public static uint Count<T>(this IEnumerable<T> input)
        {
            IEnumerator<T> enumerator = input.GetEnumerator();
            enumerator.Reset();
            uint count = 0;
            while (enumerator.MoveNext())
                count++;
            return count;
        }

        public static T Min<T>(this IEnumerable<T> input) where T : IComparable
        {
            T current = default(T);
            foreach (T value in input)
                if (value.CompareTo(current) < 0)
                    current = value;
            return current;
        }

        public static T Max<T>(this IEnumerable<T> input) where T : IComparable
        {
            T current = default(T);
            foreach (T value in input)
                if (value.CompareTo(current) > 0)
                    current = value;
            return current;
        }
    }
}
