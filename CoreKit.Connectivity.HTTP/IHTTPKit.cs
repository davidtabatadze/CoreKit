using System.Threading.Tasks;

namespace CoreKit.Connectivity.HTTP
{

    /// <summary>
    /// Represents an interface of HttpClient manager
    /// </summary>
    public interface IHTTPKit
    {

        /// <summary>
        /// Gets T type result of http request
        /// </summary>
        /// <typeparam name="T">Type of response</typeparam>
        /// <param name="method">Request method <see cref="HTTPKitRequestMethod"/></param>
        /// <param name="url">Destination url</param>
        /// <param name="payload">Data parameters</param>
        /// <returns>Request result of T Type</returns>
        Task<HTTPKitResponse<T>> RequestAsync<T>(HTTPKitRequestMethod method, string url, object payload = null);

        /// <summary>
        /// Gets T type result of http request
        /// </summary>
        /// <typeparam name="T">Type of response</typeparam>
        /// <param name="method">Request method <see cref="HTTPKitRequestMethod"/></param>
        /// <param name="url">Destination url</param>
        /// <param name="payload">Data parameters</param>
        /// <returns>Request result of T Type</returns>
        HTTPKitResponse<T> Request<T>(HTTPKitRequestMethod method, string url, object payload = null);

    }

}