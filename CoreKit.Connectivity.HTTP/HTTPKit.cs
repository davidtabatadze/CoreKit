using System;
using System.Text;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using Newtonsoft.Json;
using CoreKit.Sync;
using CoreKit.Extension.String;
using Microsoft.Extensions.Options;

namespace CoreKit.Connectivity.HTTP
{

    /// <summary>
    /// Represents a manager for HttpClient
    /// </summary>
    public class HTTPKit : IDisposable
    {

        /// <summary>
        /// Ending class lifecycle
        /// </summary>
        public virtual void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Constructor
        /// </summary>     
        /// <param name="configuration">Configuration <see cref="HTTPKitConfiguration"/></param>
        public HTTPKit(IOptions<HTTPKitConfiguration> configuration) : this(configuration.Value)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>     
        /// <param name="configuration">Configuration <see cref="HTTPKitConfiguration"/></param>
        public HTTPKit(HTTPKitConfiguration configuration)
        {
            // ...
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration object
        /// </summary>
        private HTTPKitConfiguration Configuration { get; set; }

        /// <summary>
        /// Gets T type result of http request
        /// </summary>
        /// <typeparam name="T">Type of response</typeparam>
        /// <param name="method">Request method <see cref="HTTPKitRequestMethod"/></param>
        /// <param name="url">Destination url</param>
        /// <param name="payload">Data parameters</param>
        /// <param name="headers">Data headers</param>
        /// <returns>Request result of T Type</returns>
        public async Task<HTTPKitResponse<T>> RequestAsync<T>(HTTPKitRequestMethod method, string url, object payload = null, Dictionary<string, string> headers = null)
        {
            // Trying request
            try
            {
                // Building web proxy
                var proxy = new HttpClientHandler
                {
                    UseProxy = Configuration.UseWebProxy && Configuration.WebProxyURL.HasValue(),
                    Proxy = Configuration.WebProxyURL.HasValue() ? new WebProxy(
                        Configuration.WebProxyURL,
                        Configuration.WebProxyPort
                    ) : null
                };
                // Building http
                using (var http = new HttpClient(proxy))
                {
                    // Configuring http
                    http.BaseAddress = new Uri(Configuration.ServiceURL);
                    http.DefaultRequestHeaders.Accept.Clear();
                    http.DefaultRequestHeaders.Accept.Add(
                        new MediaTypeWithQualityHeaderValue("application/json")
                    );
                    // Fixing headers
                    headers = headers ?? new Dictionary<string, string> { };
                    // Configuring header client
                    if (Configuration.ClientHeader.HasValue() && Configuration.Client.HasValue())
                    {
                        if (!headers.ContainsKey(Configuration.ClientHeader))
                        {
                            headers.Add(Configuration.ClientHeader, Configuration.Client);
                        }
                    }
                    // Configuring header secret
                    if (Configuration.SecretHeader.HasValue() && Configuration.Secret.HasValue())
                    {
                        if (!headers.ContainsKey(Configuration.SecretHeader))
                        {
                            headers.Add(Configuration.SecretHeader, Configuration.Secret);
                        }
                    }
                    // Configuring headers
                    foreach (var header in headers)
                    {
                        http.DefaultRequestHeaders.Add(header.Key, header.Value);
                    }
                    // Defining request
                    var request = new HttpResponseMessage();
                    // GET method
                    if (method == HTTPKitRequestMethod.Get)
                    {
                        // Prepare params
                        payload = payload ?? "";
                        // Do GET
                        request = await http.GetAsync(url + payload.ToString());
                    }
                    // POST method
                    if (method == HTTPKitRequestMethod.Post)
                    {
                        // პარამეტრების მომზადება
                        var content = new StringContent(
                            JsonConvert.SerializeObject(
                                payload,
                                Formatting.None,
                                new JsonSerializerSettings
                                {
                                    NullValueHandling = NullValueHandling.Ignore
                                }
                            ),
                            Encoding.UTF8,
                            "application/json"
                        );
                        // Do POST
                        request = await http.PostAsync(url, content);
                    }
                    // Defining response
                    var response = await request.Content.ReadAsStringAsync();
                    // Defining result
                    var result = new HTTPKitResponse<T>();
                    // Filling result in case of request success
                    if (request.IsSuccessStatusCode)
                    {
                        result.Data = JsonConvert.DeserializeObject<T>(response);
                    }
                    // Filling result in case of request failure
                    else
                    {
                        result.Error = true;
                        result.ErrorText = JsonConvert.DeserializeObject<string>(response);
                        if (result.ErrorText.IsEmpty())
                        {
                            result.ErrorText = request.ReasonPhrase;
                        }
                    }
                    // Returning the result
                    return result;
                }
            }
            // Catching any process exceptions
            catch (Exception exception)
            {
                // Informing user as a result
                return new HTTPKitResponse<T>
                {
                    Error = true,
                    ErrorText = exception.Message
                };
            }
        }

        /// <summary>
        /// Gets T type result of http request
        /// </summary>
        /// <typeparam name="T">Type of response</typeparam>
        /// <param name="method">Request method <see cref="HTTPKitRequestMethod"/></param>
        /// <param name="url">Destination url</param>
        /// <param name="payload">Data parameters</param>
        /// <param name="headers">Data headers</param>
        /// <returns>Request result of T Type</returns>
        public HTTPKitResponse<T> Request<T>(HTTPKitRequestMethod method, string url, object payload = null, Dictionary<string, string> headers = null)
        {
            // Do request
            return SyncKit.Run(() => RequestAsync<T>(method, url, payload, headers));
        }

    }

}
