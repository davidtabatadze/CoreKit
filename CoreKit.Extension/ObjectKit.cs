using System.Text.Json;
using System.Reflection;
using System.Threading.Tasks;
using System.Text.Encodings.Web;

namespace CoreKit.Extension.Class
{

    /// <summary>
    /// Represents an extension for <see cref="object"/>
    /// </summary>
    public static class ObjectKit
    {

        /// <summary>
        /// Json options
        /// </summary>
        private static readonly JsonSerializerOptions JsonOptions = new JsonSerializerOptions
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
            //DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault
        };

        /// <summary>
        /// Gets Json string of the specified object
        /// </summary>
        /// <param name="source">Source object</param>
        /// <returns>Json string</returns>
        public static string ToJson(this object source)
        {
            return JsonSerializer.Serialize(source, JsonOptions);
        }

        /// <summary>
        ///  Gets typed T object from json string
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="source">Source object</param>
        /// <param name="json">Json string</param>
        /// <returns>T object</returns>
        public static T FromJson<T>(this T source, string json)
        {
            return JsonSerializer.Deserialize<T>(json, JsonOptions);
        }

        /// <summary>
        /// Gets fresh copy of any object
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="source">Source object</param>
        /// <returns>Fresh copy of object</returns>
        public static T Clone<T>(this T source)
        {
            // In case if null
            if (source == null)
            {
                // Returning input
                return source;
            }
            // Serialize object to json
            var json = JsonSerializer.Serialize(source, JsonOptions);
            // Deserialize as fresh copy
            return JsonSerializer.Deserialize<T>(json, JsonOptions);
        }

        /// <summary>
        /// ???
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="N"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static N CloneTo<T, N>(this T source)
        {
            return default(N);
        }

        /// <summary>
        /// Invokes (calls) nonepublic method
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <typeparam name="R">Return type of nonpublic method</typeparam>
        /// <param name="source">Source object</param>
        /// <param name="method">Name of nonpublic method</param>
        /// <param name="parameters">Paremeters of nonpublic method</param>
        /// <returns>The result of execution of nonpublic method</returns>
        public static R InvokeNonPublic<T, R>(this T source, string method, object[] parameters)
        {
            return (R)source.GetType()
                            .GetMethod(method, BindingFlags.NonPublic | BindingFlags.Instance)
                            .Invoke(source, parameters);
        }

        /// <summary>
        /// Invokes (calls) nonepublic asynchronous method
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <typeparam name="R">Return type of nonpublic method</typeparam>
        /// <param name="source">Source object</param>
        /// <param name="method">Name of nonpublic method</param>
        /// <param name="parameters">Paremeters of nonpublic method</param>
        /// <returns>The result of execution of nonpublic method</returns>
        public static async Task<R> InvokeNonPublicAsync<T, R>(this T source, string method, object[] parameters)
        {
            return await (Task<R>)source.GetType()
                                        .GetMethod(method, BindingFlags.NonPublic | BindingFlags.Instance)
                                        .Invoke(source, parameters);
        }

        /// <summary>
        /// Invokes (calls) nonepublic method
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="source">Source object</param>
        /// <param name="method">Name of nonpublic method</param>
        /// <param name="parameters">Paremeters of nonpublic method</param>
        public static void InvokeNonPublic<T>(this T source, string method, object[] parameters)
        {
            source.GetType()
                  .GetMethod(method, BindingFlags.NonPublic | BindingFlags.Instance)
                  .Invoke(source, parameters);
        }

        /// <summary>
        /// Invokes (calls) nonepublic asynchronous method
        /// </summary>
        /// <typeparam name="T">Type of object</typeparam>
        /// <param name="source">Source object</param>
        /// <param name="method">Name of nonpublic method</param>
        /// <param name="parameters">Paremeters of nonpublic method</param>
        /// <returns>Empty</returns>
        public static async Task InvokeNonPublicAsync<T>(this T source, string method, object[] parameters)
        {
            await (Task)source.GetType()
                              .GetMethod(method, BindingFlags.NonPublic | BindingFlags.Instance)
                              .Invoke(source, parameters);
        }

    }

}
