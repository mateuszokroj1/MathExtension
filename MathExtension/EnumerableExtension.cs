using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace MathExtension.EnumerableExtension
{
    public static class EnumerableExtension
    {
        public static IEnumerable<T> BubbleSort<T>(this IEnumerable<T> input) where T : IComparable
        {
            if (input == null || input.Count() < 2) return input;
            T[] ret = (T[])input;
            bool changed = true;
            for (uint i = 0; i <= ret.Length && !changed; i++)
            {
                changed = false;
                for (uint index = 0; index < ret.Length - 1; index++)
                {
                    if (ret[index].CompareTo(ret[index + 1]) > 0)
                    {
                        T first = ret[index], second = ret[index + 1];
                        ret[index] = second;
                        ret[index + 1] = first;
                        changed = true;
                    }
                }
            }
            return ret;
        }

        public static IEnumerable<byte> BucketSort<T>(this IEnumerable<byte> input)
        {
            if (input == null || input.Count() < 2) return input;
            byte max = input.Max();
            int shift = input.Min();
            uint[] values = new uint[max-shift];
            foreach (byte b in input)
                values[b - shift-1]++;
            byte[] ret = new byte[values.Sum()];
            for(uint i = 0; i < ret.Length; i++)
            {
                for(uint j = 0; i < values.Length; i++)
                {
                    if(values[j] > 0)
                    {
                        values[j]--;
                        ret[i] = Convert.ToByte(i) + Convert.ToByte(shift);
                        break;
                    }
                }
            }
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

        public static byte Sum(this IEnumerable<byte> input)
        {
            byte val = 0;
            foreach (var item in input)
                val += item;
            return val;
        }

        public static sbyte Sum(this IEnumerable<sbyte> input)
        {
            sbyte val = 0;
            foreach (var item in input)
                val += item;
            return val;
        }

        public static short Sum(this IEnumerable<short> input)
        {
            short val = 0;
            foreach (var item in input)
                val += item;
            return val;
        }

        public static ushort Sum(this IEnumerable<ushort> input)
        {
            ushort val = 0;
            foreach (var item in input)
                val += item;
            return val;
        }

        public static int Sum(this IEnumerable<int> input)
        {
            int val = 0;
            foreach (var item in input)
                val += item;
            return val;
        }

        public static uint Sum(this IEnumerable<uint> input)
        {
            uint val = 0;
            foreach (var item in input)
                val += item;
            return val;
        }
        public static long Sum(this IEnumerable<long> input)
        {
            long val = 0;
            foreach (var item in input)
                val += item;
            return val;
        }
        public static ulong Sum(this IEnumerable<ulong> input)
        {
            ulong val = 0;
            foreach (var item in input)
                val += item;
            return val;
        }
        public static float Sum(this IEnumerable<float> input)
        {
            float val = 0;
            foreach (var item in input)
                val += item;
            return val;
        }
        public static double Sum(this IEnumerable<double> input)
        {
            double val = 0;
            foreach (var item in input)
                val += item;
            return val;
        }
    }
}
