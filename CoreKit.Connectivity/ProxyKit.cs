using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace CoreKit.Connectivity
{

    /// <summary>
    /// Represents a manager for HttpClient proxy
    /// </summary>
    public class ProxyKit : IDisposable
    {

        /// <summary>
        /// Dispose
        /// </summary>
        public virtual void Dispose()
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>     
        /// <param name="configuration">Configuration <see cref="ProxyKitConfiguration"/></param>
        public ProxyKit(IOptions<ProxyKitConfiguration> configuration) : this(configuration.Value)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>     
        /// <param name="configuration">Configuration <see cref="ProxyKitConfiguration"/></param>
        public ProxyKit(ProxyKitConfiguration configuration)
        {
            // ...
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration object
        /// </summary>
        private ProxyKitConfiguration Configuration { get; set; }

        /// <summary>
        /// Gets T type result of http request
        /// </summary>
        /// <typeparam name="T">Type of response</typeparam>
        /// <param name="method">Request method <see cref="ProxyKitRequestMethod"/></param>
        /// <param name="url">Destination url</param>
        /// <param name="payload">Data parameters</param>
        /// <param name="headers">Data headers</param>
        /// <returns>Request result of T Type</returns>
        public async Task<ProxyKitResponse<T>> Request<T>(ProxyKitRequestMethod method, string url, object payload = null, Dictionary<string, string> headers = null)
        {
            // Building web proxy according configuration
            var httpClientHandler = new HttpClientHandler
            {
                UseProxy = Configuration.UseWebProxy,
                Proxy = new WebProxy(
                    Configuration.WebProxyURL ?? "",
                    Configuration.WebProxyPort
                )
            };
            // Building client
            using (var httpClient = new HttpClient(httpClientHandler))
            {
                //oy2itsvhemyxjymnylhayt2dzndw2l7eng3va5ng6i6wwy
            }
            return null;
        }

    }

}
