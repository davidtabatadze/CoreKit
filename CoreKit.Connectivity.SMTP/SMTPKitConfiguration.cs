

namespace CoreKit.Connectivity.SMTP
{

    /// <summary>
    /// Represents a configuration for SMTP <see cref="SMTPKit"/>
    /// </summary>
    public class SMTPKitConfiguration
    {

        /// <summary>
        /// Server address
        /// </summary>
        public string Server { get; set; }

        /// <summary>
        /// Server port
        /// </summary>
        public int Port { get; set; }

        /// <summary>
        /// User
        /// </summary>
        public string User { get; set; }

        /// <summary>
        /// Password
        /// </summary>
        public string Password { get; set; }

        /// <summary>
        /// Sender mail address. If empty "User" will be used
        /// </summary>
        public string Sender { get; set; }

        /// <summary>
        /// Whether use SSL or not
        /// </summary>
        public bool EnableSSL { get; set; }

    }

}
