using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using CoreKit.Extension.String;

namespace CoreKit.Extension
{

    /// <summary>
    /// Represents an extension for <see cref="File"/>
    /// </summary>
    public static class FileKit
    {

        /// <summary>
        /// Tries to read all the text from file
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="exception">Exception message</param>
        /// <param name="encoding">File encoding</param>
        /// <returns>File text</returns>
        public static async Task<string> TryReadAllTextAsync(string path, string exception = null, Encoding encoding = null)
        {
            var result = string.Empty;
            try
            {
                result = await File.ReadAllTextAsync(path, encoding ?? Encoding.Default);
            }
            catch
            {
                if (exception.HasValue())
                {
                    throw new Exception(exception);
                }
            }
            return result;
        }

        /// <summary>
        /// Tries to read all the text from file
        /// </summary>
        /// <param name="path">File path</param>
        /// <param name="exception">Exception message</param>
        /// <param name="encoding">File encoding</param>
        /// <returns>File text</returns>
        public static string TryReadAllText(string path, string exception = null, Encoding encoding = null)
        {
            var result = string.Empty;
            try
            {
                result = File.ReadAllText(path, encoding ?? Encoding.Default);
            }
            catch
            {
                if (exception.HasValue())
                {
                    throw new Exception(exception);
                }
            }
            return result;
        }

    }

}
