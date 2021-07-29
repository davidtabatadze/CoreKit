using System.Text.Json;
using System.Reflection;
using System.Threading.Tasks;

namespace CoreKit.Extension.Class
{

    /// <summary>
    /// Represents an extension for <see cref="object"/>
    /// </summary>
    public static class ObjectKit
    {

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
            var json = JsonSerializer.Serialize(source);
            // Deserialize as fresh copy
            return JsonSerializer.Deserialize<T>(json);
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
