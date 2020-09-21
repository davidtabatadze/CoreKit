using System.Collections.Generic;

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
        /// whether use web proxy or not
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
        /// Default headers of request
        /// </summary>
        public Dictionary<string, string> Headers { get; set; }

    }

}
