using System;
using System.Text;
using System.Linq;
using System.Security.Cryptography;

namespace CoreKit.Extension.String
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
        /// Get the string transformed to upper camel case
        /// </summary>
        /// <param name="source">Source string</param>
        /// <returns>Transformed string</returns>
        public static string ToPascal(this string source)
        {
            return source[0].ToString().ToUpper() + source.Remove(0, 1);
        }

        /// <summary>
        /// Get the string transformed to lower camel case
        /// </summary>
        /// <param name="source">Source string</param>
        /// <returns>Transformed string</returns>
        public static string ToDromedary(this string source)
        {
            return source[0].ToString().ToLower() + source.Remove(0, 1);
        }

        /// <summary>
        /// Gets the string transofmed to SHA1 hash
        /// </summary>
        /// <param name="source">Source string</param>
        /// <returns>SHA1 hash</returns>
        public static string ToSHA1(this string source)
        {
            return BitConverter.ToString(
                new SHA1Managed().ComputeHash(
                    Encoding.UTF8.GetBytes(source)
                )
            ).Replace("-", "").ToLower();
        }

        /// <summary>
        /// Get the string trimmed every possible way
        /// </summary>
        /// <param name="source">Source string</param>
        /// <returns>Fully treamed string</returns>
        public static string TrimFull(this string source)
        {
            return source.IsEmpty() ? source : string.Join(
                " ",
                source.Split(' ')
                      .Where(w => w.HasValue())
                      .Select(w => w.Trim())
            );
        }

        /// <summary>
        /// Get lower string trimmed every possible way
        /// </summary>
        /// <param name="source">Source string</param>
        /// <returns>Fully treamed string</returns>
        public static string TrimFullAndLower(this string source)
        {
            return source.IsEmpty() ? source : source.TrimFull().ToLower();
        }

        /// <summary>
        /// Get upper string trimmed every possible way
        /// </summary>
        /// <param name="source">Source string</param>
        /// <returns>Fully treamed string</returns>
        public static string TrimFullAndUpper(this string source)
        {
            return source.IsEmpty() ? source : source.TrimFull().ToUpper();
        }

    }

}
