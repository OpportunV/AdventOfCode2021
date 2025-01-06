using System;
using System.Collections.Generic;
using System.Linq;

namespace AdventOfCode2021.Helpers
{
    public static class Extensions
    {
        public static string Sorted(this string s)
        {
            return string.Concat(s.OrderBy(c => c));
        }
        
        public static string Reversed(this string str)
        {
            var chars = str.ToCharArray();
            Array.Reverse(chars);
            return new string(chars);
        }

        public static string[] Rotated(this string[] array)
        {
            var width = array.Length;
            var height = array[0].Length;
            var src = array.Select(item => item.ToArray()).ToArray();
            
            var dst = new char[height][];
            for (int i = 0; i < height; i++)
            {
                dst[i] = new char[width];
            }

            for (int row = 0; row < height; row++)
            {
                for (int col = 0; col < width; col++)
                {
                    var newRow = width - col - 1;
                    var newCol = row;

                    dst[newCol][newRow] = src[col][row];
                }
            }

            var result = new string[height];
            for (int i = 0; i < height; i++)
            {
                result[i] = new string(dst[i]);
            }

            return result;
        }

        public static TValue GetOrDefault<TKey, TValue>(this Dictionary<TKey, TValue> dictionary, TKey key, TValue @default)
        {
            if (dictionary.ContainsKey(key))
            {
                return dictionary[key];
            }

            return @default;
        }

        public static (int, int) Add(this (int, int) self, (int, int) other)
        {
            var (item1, item2) = self;
            var (i, item3) = other;
            return (item1 + i, item2 + item3);
        }
        
        public static IEnumerable<(T first, T second)> DoubleIteration<T>(this IEnumerable<T> enumerable)
        {
            var array = enumerable as T[] ?? enumerable.ToArray();
            for (var i = 0; i < array.Length; i++)
            {
                var first = array[i];
                for (var j = 0; j < array.Length; j++)
                {
                    if (i == j)
                    {
                        continue;
                    }
                    var second = array[j];
                    yield return (first, second);
                }
            }
        }
    }
}