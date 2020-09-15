using System;
using System.Linq;
using System.Collections.Generic;

namespace CoreKit.Extension.Collection
{

    /// <summary>
    /// Represents an extension for collections
    /// </summary>
    public static class CollectionKit
    {

        /// <summary>
        /// Gets result indicating whether the collection is empty or not.
        /// </summary>
        /// <typeparam name="T">Type of IEnumerable</typeparam>
        /// <param name="source">Source collection</param>
        /// <returns>Yes/No result</returns>
        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || source.Count() == 0;
        }

        /// <summary>
        /// Gets result indicating whether the collection has at least one item or not.
        /// </summary>
        /// <typeparam name="T">Type of IEnumerable</typeparam>
        /// <param name="source">Source collection</param>
        /// <returns>Yes/No result</returns>
        public static bool HasValue<T>(this IEnumerable<T> source)
        {
            return !source.IsEmpty();
        }

        /// <summary>
        /// Gets source list without empty elements
        /// </summary>
        /// <typeparam name="T">Type of list</typeparam>
        /// <param name="source">Source list</param>
        /// <returns>Trimmed list</returns>
        public static List<T> TrimEmpty<T>(this List<T> source)
        {
            // In case if source is not presented
            if (source.IsEmpty())
            {
                // We return empty list
                return new List<T> { };
            }
            // If source list type is specifically string
            if (typeof(T) == typeof(string))
            {
                // We are removing null or empty values
                source.RemoveAll(r => string.IsNullOrWhiteSpace(r as string));
            }
            // For every other types
            else
            {
                // Remove null values
                source.RemoveAll(r => r == null);
            }
            // Returning trimmed list
            return source;
        }

        /// <summary>
        /// Gets source list without empty elements or without lte zero elements
        /// </summary>
        /// <typeparam name="T">Type of list</typeparam>
        /// <param name="source">Source list</param>
        /// <returns>Trimmed list</returns>
        public static List<T> TrimEmptyOrLTE0<T>(this List<T> source)
        {
            // First trim empty
            source.TrimEmpty();
            // In case if after trimming source is valid,
            // we do extra trimming
            if (source.HasValue())
            {
                // Possible numeric types
                var numerics = new List<Type>
                {
                    typeof(short),
                    typeof(int),
                    typeof(long),
                    typeof(float),
                    typeof(double),
                    typeof(decimal),
                    typeof(byte),
                    typeof(sbyte),
                    typeof(ushort),
                    typeof(ulong),
                    typeof(uint)
                };
                // If source is numeric type or nullable numeric type
                if (numerics.Contains(typeof(T)) || numerics.Contains(Nullable.GetUnderlyingType(typeof(T))))
                {
                    // We do trim further
                    source.RemoveAll(r => Convert.ToDouble(r) <= 0.0);
                }
            }
            // Returning trimmed list
            return source;
        }

    }

}
