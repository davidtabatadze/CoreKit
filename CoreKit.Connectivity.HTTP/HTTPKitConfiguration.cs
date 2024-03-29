﻿using System.Collections.Generic;

namespace CoreKit.Connectivity.HTTP
{

    /// <summary>
    /// Represents a configuration for http kit <see cref="HTTPKit"/>
    /// </summary>
    public class HTTPKitConfiguration
    {

        /// <summary>
        /// Address
        /// </summary>
        public string ServiceURL { get; set; }

        /// <summary>
        /// Whether to use web proxy or not
        /// </summary>
        public bool UseWebProxy { get; set; }

        /// <summary>
        /// Web proxy address
        /// </summary>
        public string WebProxyURL { get; set; }

        /// <summary>
        /// Web proxy port
        /// </summary>
        public int WebProxyPort { get; set; }

        /// <summary>
        /// Whether to trust detected certificate or not
        /// </summary>
        public bool TrustCertificate { get; set; }

        /// <summary>
        /// If this is true, the raw response will allways be included. Otherwise only when Error occurs
        /// </summary>
        public bool IncludeRawResponse { get; set; }

        /// <summary>
        /// Milliseconds to timeout the request
        /// </summary>
        public int TimeoutMilliseconds { get; set; }

        /// <summary>
        /// Whether to use upper camel case during deserialization
        /// </summary>
        public bool UsePascalNaming { get; set; }

        /// <summary>
        /// Only 200 response code will be considered as success
        /// </summary>
        public bool SuccessIs200Only { get; set; }

        /// <summary>
        /// Default headers of request
        /// </summary>
        public Dictionary<string, string> Headers { get; set; }

    }

}
