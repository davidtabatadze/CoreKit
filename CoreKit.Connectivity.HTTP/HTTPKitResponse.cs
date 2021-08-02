

namespace CoreKit.Connectivity.HTTP
{

    /// <summary>
    /// Represents response model of proxy kit <see cref="HTTPKit"/>
    /// </summary>
    /// <typeparam name="T">Type of response data / value</typeparam>
    public class HTTPKitResponse<T>
    {

        /// <summary>
        /// Whether proxy request succeeded or not
        /// </summary>
        public bool Error { get; set; }

        /// <summary>
        /// Error message in case of failed proxy request
        /// </summary>
        public string ErrorText { get; set; }

        /// <summary>
        /// Raw responce, in case if response is not convertable to T
        /// </summary>
        public string RawData { get; set; }

        /// <summary>
        /// Proxy request result value
        /// </summary>
        public T Data { get; set; }

    }

}
