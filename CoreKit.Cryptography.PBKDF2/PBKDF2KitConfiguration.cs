

namespace CoreKit.Cryptography.PBKDF2
{

    /// <summary>
    /// Represents a configuration for PBKDF2 <see cref="PBKDF2Kit"/>
    /// </summary>
    public class PBKDF2KitConfiguration
    {

        /// <summary>
        /// Hash iterations
        /// </summary>
        public int HashIterations { get; set; }

        /// <summary>
        /// Salt size
        /// </summary>
        public int SaltSize { get; set; }

    }

}
