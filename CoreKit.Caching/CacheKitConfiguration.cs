

namespace CoreKit.Caching
{

    /// <summary>
    /// Represents a configuration for cache manager <see cref="CacheKit"/>
    /// </summary>
    public class CacheKitConfiguration
    {

        /// <summary>
        /// Default minutes for data to be cached.
        /// 0 - means default 60 minutes.
        /// </summary>
        public uint DefaultCachingMinutes { get; set; }

    }

}
