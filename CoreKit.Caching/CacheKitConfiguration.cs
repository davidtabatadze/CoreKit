

namespace CoreKit.Caching
{

    /// <summary>
    /// Represents a configuration for cache manager <see cref="CacheKit"/>
    /// </summary>
    public class CacheKitConfiguration
    {

        /// <summary>
        /// Default minutes for data to be cached.
        /// </summary>
        /// <remarks>0 means no cache.</remarks>
        public uint DefaultCachingMinutes { get; set; }

    }

}
