using System;
using System.Net;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;

namespace CoreKit.Connectivity.SMTP
{

    /// <summary>
    /// Represents a manager for SMTP Client
    /// </summary>
    public class SMTPKit : IDisposable
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
        /// <param name="configuration">Configuration <see cref="SMTPKitConfiguration"/></param>
        public SMTPKit(IOptions<SMTPKitConfiguration> configuration) : this(configuration.Value)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">Configuration <see cref="SMTPKitConfiguration"/></param>
        public SMTPKit(SMTPKitConfiguration configuration)
        {
            // ...
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration object
        /// </summary>
        private SMTPKitConfiguration Configuration { get; set; }

        /// <summary>
        /// Gets composed mail for delivery
        /// </summary>
        /// <param name="destination">Destination mail address</param>
        /// <param name="subject">Mail subject title</param>
        /// <param name="body">Mail body content</param>
        /// <param name="html">Whether HTML or not</param>
        /// <returns>Mail for delivery</returns>
        private MailMessage ComposeMail(string destination, string subject, string body, bool html = false)
        {
            // Compose mail and return
            var mail = new MailMessage { };
            mail.From = new MailAddress(Configuration.Sender ?? Configuration.User);
            mail.To.Add(destination);
            mail.Subject = subject;
            mail.IsBodyHtml = html;
            mail.Body = body;
            return mail;
        }

        /// <summary>
        /// Gets configured SMTP client
        /// </summary>
        /// <returns>SMTP client</returns>
        private SmtpClient ConfigureClient()
        {
            // Configure SMTP and return
            return new SmtpClient(Configuration.Server)
            {
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential(Configuration.User, Configuration.Password),
                Port = Configuration.Port,
                EnableSsl = Configuration.EnableSSL
            };
        }

        /// <summary>
        /// Sends mail via SMTP
        /// </summary>
        /// <param name="destination">Destination mail address</param>
        /// <param name="subject">Mail subject title</param>
        /// <param name="body">Mail body content</param>
        /// <param name="html">Whether HTML or not</param>
        /// <returns>Nothing</returns>
        public async Task SendAsync(string destination, string subject, string body, bool html = false)
        {
            await ConfigureClient().SendMailAsync(ComposeMail(destination, subject, body, html));
        }

        /// <summary>
        /// Sends mail via SMTP
        /// </summary>
        /// <param name="destination">Destination mail address</param>
        /// <param name="subject">Mail subject title</param>
        /// <param name="body">Mail body content</param>
        /// <param name="html">Whether HTML or not</param>
        public void Send(string destination, string subject, string body, bool html = false)
        {
            ConfigureClient().Send(ComposeMail(destination, subject, body, html));
        }

    }

}
