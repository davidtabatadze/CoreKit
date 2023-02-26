using System;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Encodings.Web;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using System.Collections.Generic;
using CoreKit.Sync;
using Microsoft.Extensions.Options;

namespace CoreKit.Connectivity.HTTP
{

    /// <summary>
    /// Represents a manager for HttpClient
    /// </summary>
    public class HTTPKit : IHTTPKit, IDisposable
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
            // Cancellation = new CancellationTokenSource(configuration.TimeoutMilliseconds).Token;
            JsonOptions = new JsonSerializerOptions
            {
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingDefault,
                PropertyNamingPolicy = configuration.UsePascalNaming ? JsonNamingPolicy.CamelCase : null
            };
            Configuration = configuration;
        }

        /// <summary>
        /// Json options
        /// </summary>
        private readonly JsonSerializerOptions JsonOptions = null;

        /// <summary>
        /// Configuration object
        /// </summary>
        private readonly HTTPKitConfiguration Configuration = null;

        /// <summary>
        /// object locker
        /// </summary>
        private readonly object locker = new object();

        /// <summary>
        /// HTTP instance
        /// </summary>
        private HttpClient Instance { get; set; }

        /// <summary>
        /// Singleton HTTP instance
        /// </summary>
        private HttpClient HTTP
        {
            get
            {
                // In case the instance is not ready...
                if (Instance == null)
                {
                    // Locking for thread safety
                    lock (locker)
                    {
                        // Creating instance
                        if (Instance == null)
                        {
                            // Building handler
                            var handler = new HttpClientHandler
                            {
                                UseProxy = Configuration.UseWebProxy && (!string.IsNullOrWhiteSpace(Configuration.WebProxyURL)),
                                Proxy = string.IsNullOrWhiteSpace(Configuration.WebProxyURL) ?
                                        null :
                                        new WebProxy(
                                            Configuration.WebProxyURL,
                                            Configuration.WebProxyPort
                                        )
                            };
                            if (Configuration.TrustCertificate)
                            {
                                handler.ClientCertificateOptions = ClientCertificateOption.Manual;
                                handler.ServerCertificateCustomValidationCallback +=
                                    (sender, certificate, chain, errors) =>
                                    {
                                        return true;
                                    };
                            }
                            // Building http
                            var http = new HttpClient(handler);
                            // Configuring http
                            http.BaseAddress = new Uri(Configuration.ServiceURL);
                            //http.DefaultRequestHeaders.ConnectionClose = true;
                            http.DefaultRequestHeaders.Accept.Clear();
                            http.DefaultRequestHeaders.Accept.Add(
                                new MediaTypeWithQualityHeaderValue("application/json")
                            );
                            // Applying configuration headers
                            Configuration.Headers = Configuration.Headers ?? new Dictionary<string, string> { };
                            foreach (var header in Configuration.Headers)
                            {
                                http.DefaultRequestHeaders.Add(header.Key, header.Value);
                            }
                            // Applying the instance
                            Instance = http;
                        }
                    }
                }
                // Returning singleton
                return Instance;
            }
        }

        /// <summary>
        /// Gets T type result of http request
        /// </summary>
        /// <typeparam name="T">Type of response</typeparam>
        /// <param name="method">Request method <see cref="HTTPKitRequestMethod"/></param>
        /// <param name="url">Destination url</param>
        /// <param name="payload">Data parameters</param>
        /// <returns>Request result of T Type</returns>
        public async Task<HTTPKitResponse<T>> RequestAsync<T>(HTTPKitRequestMethod method, string url, object payload = null)
        {
            // Trying request
            try
            {
                // Defining request
                var request = new HttpResponseMessage();
                // Defining content
                var content = payload == null ? null : new StringContent(
                    JsonSerializer.Serialize(
                        payload,
                        typeof(object),
                        JsonOptions
                    ),
                    Encoding.UTF8,
                    "application/json"
                );
                // GET method
                if (method == HTTPKitRequestMethod.GET)
                {
                    // Prepare params
                    payload ??= "";
                    // Do GET
                    request = await HTTP.GetAsync(url + payload.ToString());
                }
                // PUT method
                if (method == HTTPKitRequestMethod.PUT)
                {
                    throw new NotImplementedException();
                }
                // POST method
                if (method == HTTPKitRequestMethod.POST)
                {
                    request = await HTTP.PostAsync(url, content);
                }
                // DELETE method
                if (method == HTTPKitRequestMethod.DELETE)
                {
                    request = await HTTP.SendAsync(new HttpRequestMessage
                    {
                        Content = content,
                        Method = HttpMethod.Delete,
                        RequestUri = new Uri(Configuration.ServiceURL + url)
                    });
                }
                // Defining response
                var response = await request.Content.ReadAsStringAsync();
                // Defining result
                var result = new HTTPKitResponse<T>
                {
                    Code = (int)request.StatusCode
                };
                // Defining success
                var success = request.IsSuccessStatusCode &&
                              (Configuration.SuccessIs200Only == false || request.StatusCode == HttpStatusCode.OK);
                // Filling result in case of request success
                if (success)
                {
                    try
                    {
                        result.Data = JsonSerializer.Deserialize<T>(response, JsonOptions);
                    }
                    catch
                    {
                        result.Error = true;
                        result.ErrorText = "Response not deserializable to " + typeof(T).FullName;
                    }
                    finally
                    {
                        if (result.Error || Configuration.IncludeRawResponse)
                        {
                            result.RawData = response;
                        }
                    }
                }
                // Filling result in case of request failure
                else
                {
                    result.Error = true;
                    result.ErrorText = response;
                    if (string.IsNullOrWhiteSpace(result.ErrorText))
                    {
                        result.ErrorText = request.ReasonPhrase;
                    }
                }
                // Returning the result
                return result;
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
        /// <returns>Request result of T Type</returns>
        public HTTPKitResponse<T> Request<T>(HTTPKitRequestMethod method, string url, object payload = null)
        {
            // Do request
            return SyncKit.Run(() => RequestAsync<T>(method, url, payload));
        }

    }

}
