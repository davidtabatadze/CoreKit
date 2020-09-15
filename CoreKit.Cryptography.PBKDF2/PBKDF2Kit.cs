using System;
using System.Text;
using System.Security.Cryptography;
using Microsoft.Extensions.Options;

namespace CoreKit.Cryptography.PBKDF2
{

    /// <summary>
    /// Represents a manager for PBKDF2
    /// </summary>
    public class PBKDF2Kit : IDisposable
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
        /// <param name="configuration">Configuration <see cref="PBKDF2KitConfiguration"/></param>
        public PBKDF2Kit(IOptions<PBKDF2KitConfiguration> configuration) : this(configuration.Value)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">Configuration <see cref="PBKDF2KitConfiguration"/></param>
        public PBKDF2Kit(PBKDF2KitConfiguration configuration)
        {
            // ...
            configuration.SaltSize = configuration.SaltSize <= 0 ? 8 : configuration.SaltSize;
            configuration.HashIterations = configuration.HashIterations <= 0 ? 1024 : configuration.HashIterations;
            Configuration = configuration;
        }

        /// <summary>
        /// Constructor (HashIterations = 100000, SaltSize = 34)
        /// </summary>
        public PBKDF2Kit()
        {
            // ...
            Configuration = new PBKDF2KitConfiguration { HashIterations = 65536, SaltSize = 32 };
        }

        /// <summary>
        /// Configuration object
        /// </summary>
        private PBKDF2KitConfiguration Configuration { get; set; }

        /// <summary>
        /// Get fresh salt using configured size and iterations
        /// </summary>
        public string Salt
        {
            get
            {
                var random = RandomNumberGenerator.Create();
                var bytes = new byte[Configuration.SaltSize];
                random.GetBytes(bytes);
                var base64 = Convert.ToBase64String(bytes);
                return string.Format("{0}.{1}", Configuration.HashIterations, base64);
            }
        }

        /// <summary>
        /// Computes the hash using given salt
        /// </summary>
        /// <param name="text">The text to be hashed</param>
        /// <param name="salt">Precomputed salt</param>
        /// <returns>Hashed result of the text</returns>
        public string GenerateHash(string text, string salt)
        {
            var bytes = Encoding.UTF8.GetBytes(salt);
            var pbkdf2 = new Rfc2898DeriveBytes(text, bytes, Configuration.HashIterations);
            var key = pbkdf2.GetBytes(64);
            return Convert.ToBase64String(key);
        }

        /// <summary>
        /// Computes the hash
        /// </summary>
        /// <param name="text">The text to be hashed</param>
        /// <returns>Hashed result of the text</returns>
        public string GenerateHash(string text)
        {
            return GenerateHash(text, Salt);
        }

        /// <summary>
        /// Compares hashes on equality
        /// </summary>
        /// <param name="hashsource">Source hash value</param>
        /// <param name="hashdestination">Destination hash value</param>
        /// <returns>Yes/No result</returns>
        public bool Compare(string hashsource, string hashdestination)
        {
            if (string.IsNullOrWhiteSpace(hashsource) || string.IsNullOrWhiteSpace(hashdestination))
            {
                return false;
            }
            var length = Math.Min(hashsource.Length, hashdestination.Length);
            var result = 0;
            for (int i = 0; i < length; i++)
            {
                result |= hashsource[i] ^ hashdestination[i];
            }
            return 0 == result;
        }

    }

}
