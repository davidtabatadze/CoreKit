

namespace CoreKit.Connectivity
{

    /// <summary>
    /// Represents response model of proxy kit <see cref="ProxyKit"/>
    /// </summary>
    /// <typeparam name="T">Type of response data / value</typeparam>
    public class ProxyKitResponse<T>
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
        /// Proxy request result value
        /// </summary>
        public T Data { get; set; }

    }

}