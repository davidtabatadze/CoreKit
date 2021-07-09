using System.Threading.Tasks;

namespace CoreKit.Connectivity.SMTP
{

    /// <summary>
    /// Represents basic description of <see cref="SMTPKit"/>
    /// </summary>
    public interface ISMTPKit
    {

        /// <summary>
        /// Sends mail via SMTP
        /// </summary>
        /// <param name="destination">Destination mail address</param>
        /// <param name="subject">Mail subject title</param>
        /// <param name="body">Mail body content</param>
        /// <param name="html">Whether HTML or not</param>
        /// <returns>Nothing</returns>
        Task SendAsync(string destination, string subject, string body, bool html = false);

        /// <summary>
        /// Sends mail via SMTP
        /// </summary>
        /// <param name="destination">Destination mail address</param>
        /// <param name="subject">Mail subject title</param>
        /// <param name="body">Mail body content</param>
        /// <param name="html">Whether HTML or not</param>
        void Send(string destination, string subject, string body, bool html = false);

    }

}
