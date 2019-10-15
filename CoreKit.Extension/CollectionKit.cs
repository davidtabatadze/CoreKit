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
        /// Gets result indicating whether the collection is empty
        /// </summary>
        /// <typeparam name="T">Type of IEnumerable</typeparam>
        /// <param name="source">Source collection</param>
        /// <returns>Yes/No result</returns>
        public static bool IsEmpty<T>(this IEnumerable<T> source)
        {
            return source == null || source.Count() == 0;
        }

        /// <summary>
        /// Gets result indicating whether the collection has at least one item
        /// </summary>
        /// <typeparam name="T">Type of  IEnumerable</typeparam>
        /// <param name="source">Source collection</param>
        /// <returns>Yes/No result</returns>
        public static bool HasValue<T>(this IEnumerable<T> source)
        {
            return !source.IsEmpty();
        }

    }

}
