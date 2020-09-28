using System;
using System.Threading.Tasks;
using System.Runtime.Caching;
using Microsoft.Extensions.Options;

namespace CoreKit.Caching
{

    /// <summary>
    /// Represents a manager for MemoryCache caching
    /// </summary>
    public class CacheKit : IDisposable
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
        /// <param name="configuration">Configuration <see cref="CacheKitConfiguration"/></param>
        public CacheKit(IOptions<CacheKitConfiguration> configuration) : this(configuration.Value)
        {
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">Configuration <see cref="CacheKitConfiguration"/></param>
        public CacheKit(CacheKitConfiguration configuration)
        {
            // Setting default cacheing minutes
            if (configuration.DefaultCachingMinutes <= 0)
            {
                configuration.DefaultCachingMinutes = 60;
            }
            // ...
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration object
        /// </summary>
        private CacheKitConfiguration Configuration { get; set; }

        /// <summary>
        /// Cache object
        /// </summary>
        private ObjectCache Cache { get { return MemoryCache.Default; } }

        /// <summary>
        /// Gets result indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">Key of the chached value</param>
        /// <returns>Yes/No result</returns>
        private bool IsSet(string key)
        {
            return Cache.Contains(key);
        }

        /// <summary>
        /// Adds the specified key and value to the cache
        /// </summary>
        /// <param name="key">Key of the value to cache</param>
        /// <param name="data">value of object to be cached</param>
        /// <param name="minutes">Minutes to maintain cached value</param>
        public void Set(string key, object data, int? minutes = null)
        {
            // In case the data is not present,
            if (data == null)
            {
                // nothing will be cached
                return;
            }
            // First removing existing value of given key
            Cache.Remove(key);
            // Determining expiration
            minutes = minutes ?? Configuration.DefaultCachingMinutes;
            var expiration = DateTime.Now + TimeSpan.FromMinutes(minutes.Value);
            // Caching the data with composed policy
            Cache.Add(
                new CacheItem(key, data),
                new CacheItemPolicy { AbsoluteExpiration = expiration }
            );
        }

        /// <summary>
        /// Get a cached value
        /// </summary>
        /// <typeparam name="T">Type of the chached value</typeparam>
        /// <param name="key">Key of the chached value</param>
        /// <returns>Cached value associated with the specified key</returns>
        public T Get<T>(string key)
        {
            return (T)Cache[key];
        }

        /// <summary>
        /// Get a cached value. If it's not there, first load and cache it
        /// </summary>
        /// <typeparam name="T">Type of the chached value</typeparam>
        /// <param name="key">Key of the chached value</param>
        /// <param name="acquire">Function to load value if it's not in the cache yet</param>
        /// <returns>Cached value associated with the specified key</returns>
        public async Task<T> Get<T>(string key, Func<Task<T>> acquire)
        {
            return await Get(key, Configuration.DefaultCachingMinutes, acquire);
        }

        /// <summary>
        /// Get a cached value. If it's not there, first load and cache it
        /// </summary>
        /// <typeparam name="T">Type of the chached value</typeparam>
        /// <param name="key">Key of the chached value</param>
        /// <param name="cacheTime">Cache time in minutes (0 - do not cache)</param>
        /// <param name="acquire">Function to load value if it's not in the cache yet</param>
        /// <returns>Cached value associated with the specified key</returns>
        public async Task<T> Get<T>(string key, int cacheTime, Func<Task<T>> acquire)
        {
            // If value is already cached at the specified key,
            // return the value
            if (IsSet(key))
            {
                return Get<T>(key);
            }
            // First execute the function to get value
            var result = await acquire();
            // Function execution result will be cached and returned
            if (cacheTime > 0)
            {
                Set(key, result, cacheTime);
            }
            return result;
        }

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">Key of the value to remove</param>
        public void Remove(string key)
        {
            // Do remove
            Cache.Remove(key);
        }

        /// <summary>
        /// Clear all cache values
        /// </summary>
        public void Clear()
        {
            // For every item in cache ...
            foreach (var item in Cache)
            {
                // Execute remove
                Remove(item.Key);
            }
        }

    }

}
