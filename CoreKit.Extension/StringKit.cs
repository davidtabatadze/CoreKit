using System;
using System.Text;
using System.Linq;
using System.Text.Json;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Security.Cryptography;

namespace CoreKit.Extension.String
{

    /// <summary>
    /// Represents an extension for <see cref="string"/>
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

        /// <summary>
        /// Get sentance of words
        /// </summary>
        /// <param name="source">Source string</param>
        /// <param name="words">Words</param>
        /// <returns>Sentance string</returns>
        public static string ToSentence(this string source, params string[] words)
        {
            source = string.Join(" ", words.Select(w => w.TrimFull()));
            return source;
        }

        /// <summary>
        /// Gets T object from Json string
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="source">Source string</param>
        /// <returns>T object</returns>
        public static T FromJson<T>(this string source)
        {
            try
            {
                return JsonSerializer.Deserialize<T>(source);
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// Gets the result indicating whether the string represents the valid email or not
        /// </summary>
        /// <param name="source">Source string</param>
        /// <returns>True/False</returns>
        public static bool IsEmail(this string source)
        {
            // Final result
            var result = false;
            // Trying to analyse input
            if (source.HasValue())
            {
                try
                {
                    // Normalize the domain
                    source = Regex.Replace(
                        source,
                        @"(@)(.+)$",
                        DomainMapper,
                        RegexOptions.None,
                        TimeSpan.FromMilliseconds(200)
                    );
                    // Examines the domain part of the email and normalizes it.
                    string DomainMapper(Match match)
                    {
                        // Use IdnMapping class to convert Unicode domain names.
                        var idn = new IdnMapping();
                        // Pull out and process domain name (throws ArgumentException on invalid)
                        string domainName = idn.GetAscii(match.Groups[2].Value);
                        // Return the result
                        return match.Groups[1].Value + domainName;
                    }
                    result = Regex.IsMatch(
                        source,
                        @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9-]+(?:\.[a-zA-Z0-9-]+)*$",
                        RegexOptions.IgnoreCase, TimeSpan.FromMilliseconds(250)
                    );
                }
                catch
                {
                }
            }
            // Returning the result
            return result;
        }

        /// <summary>
        /// Get generated random string
        /// </summary>
        /// <param name="length">Length of random result</param>
        /// <param name="useNumbers">Use numbers in random result</param>
        /// <param name="useLower">Use lower letters in random result</param>
        /// <param name="useUpper">Use upper letters in random result</param>
        /// <param name="useSpecial">Use special symbols in random result</param>
        /// <returns>Random string</returns>
        public static string Random(short length, bool useNumbers = true, bool useLower = true, bool useUpper = true, bool useSpecial = true)
        {
            var specials = "~!@#$%^&*";
            var numbers = "0123456789";
            var lowers = "abcdefghijklmnopqrstuvwxyz";
            var uppers = lowers.ToUpper();
            var pool = string.Empty;
            if (useNumbers)
            {
                pool += numbers;
            }
            if (useLower)
            {
                pool += lowers;
            }
            if (useUpper)
            {
                pool += uppers;
            }
            if (useSpecial)
            {
                pool += specials;
            }
            var result = new string(
                Enumerable.Range(0, length)
                          .Select(x => pool[new Random().Next(0, pool.Length)])
                          .ToArray()
            );
            return result;
        }

    }

}
