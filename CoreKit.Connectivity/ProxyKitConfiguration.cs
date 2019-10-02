

namespace CoreKit.Connectivity
{

    /// <summary>
    /// Represents a configuration for proxy kit <see cref="ProxyKit"/>
    /// </summary>
    public class ProxyKitConfiguration
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
        /// Client name
        /// </summary>
        public string Client { get; set; }

        /// <summary>
        /// Client name representation name in header
        /// </summary>
        public string ClientHeader { get; set; }

        /// <summary>
        /// Client secret
        /// </summary>
        public string Secret { get; set; }

        /// <summary>
        /// Client secret representation name in header
        /// </summary>
        public string SecretHeader { get; set; }

    }

}
