

namespace CoreKit.Caching
{

    /// <summary>
    /// Represents a configuration for cache manager <see cref="CacheKit"/>
    /// </summary>
    public class CacheKitConfiguration
    {

        /// <summary>
        /// Default minutes for data to be cached.
        /// 0 means cached value will be maintained while application running.
        /// </summary>
        public uint DefaultCachingMinutes { get; set; }

    }

}
