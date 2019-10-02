using System.Linq;

namespace CoreKit.Extensions.String
{

    /// <summary>
    /// Represents an extension for strings
    /// </summary>
    public static class StringKit
    {

        /// <summary>
        /// Gets result indicating whether the string is empty
        /// </summary>
        /// <param name="source">Source string</param>
        /// <returns>Yes/No result</returns>
        public static bool IsEmpty(this string source)
        {
            return source == null || string.IsNullOrEmpty(source) || string.IsNullOrWhiteSpace(source);
        }

        /// <summary>
        /// Gets result indicating whether the string has any value
        /// </summary>
        /// <param name="source">Source string</param>
        /// <returns>Yes/No result</returns>
        public static bool HasValue(this string source)
        {
            return !source.IsEmpty();
        }

        /// <summary>
        /// Get the string trimmed every possible way
        /// </summary>
        /// <param name="source">Source string</param>
        /// <returns>Fully treamed string</returns>
        public static string TrimFull(this string source)
        {
            return string.Join(
                " ",
                source.Split(' ')
                      .Where(w => w.HasValue())
                      .Select(w => w.Trim())
            );
        }

    }

}
