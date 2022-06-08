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
            // ...
            Configuration = configuration;
        }

        /// <summary>
        /// Configuration object
        /// </summary>
        private readonly CacheKitConfiguration Configuration = null;

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
        /// Adds the specified key and value to the cache. (DefaultCachingMinutes will be used)
        /// </summary>
        /// <param name="key">Key of the value to cache</param>
        /// <param name="data">value of object to be cached</param>
        public void Set(string key, object data)
        {
            Set(key, data, Configuration.DefaultCachingMinutes);
        }

        /// <summary>
        /// Adds the specified key and value to the cache
        /// </summary>
        /// <param name="key">Key of the value to cache</param>
        /// <param name="data">value of object to be cached</param>
        /// <param name="minutes">Minutes to maintain cached value</param>
        /// <remarks>
        /// minutes null - means no cache will be used.
        /// minutes 0 - means cached value will be maintained while application running.
        /// </remarks>
        public void Set(string key, object data, uint? minutes)
        {
            // In case the data ot the minutes is not present,
            if (data == null || minutes == null)
            {
                // nothing will be cached
                return;
            }
            // Defining policy
            var policy = new CacheItemPolicy { };
            // 0 means cached value will be maintained while application running
            if (minutes == 0)
            {
                policy.Priority = CacheItemPriority.NotRemovable;
            }
            // Otherwise expiration time will be set
            else
            {
                // Defining expiration
                minutes ??= Configuration.DefaultCachingMinutes;
                var expiration = DateTime.Now + TimeSpan.FromMinutes(minutes.Value);
                policy.AbsoluteExpiration = expiration;
            }
            // Removing existing value of given key
            Cache.Remove(key);
            // Caching the data with composed policy
            Cache.Add(new CacheItem(key, data), policy);
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
        /// Get a cached value. If it's not there, first load and cache it. (DefaultCachingMinutes will be used)
        /// </summary>
        /// <typeparam name="T">Type of the chached value</typeparam>
        /// <param name="key">Key of the chached value</param>
        /// <param name="acquire">Function to load value if it's not in the cache yet</param>
        /// <returns>Cached value associated with the specified key</returns>
        public async Task<T> Get<T>(string key, Func<Task<T>> acquire)
        {
            // If value is already cached at the specified key, return the value
            if (IsSet(key))
            {
                return Get<T>(key);
            }
            // First execute the function to get the value
            var result = await acquire();
            // Function value will be cached and returned
            Set(key, result);
            return result;
        }

        /// <summary>
        /// Get a cached value. If it's not there, first load and cache it
        /// </summary>
        /// <typeparam name="T">Type of the chached value</typeparam>
        /// <param name="key">Key of the chached value</param>
        /// <param name="acquire">Function to load value if it's not in the cache yet</param>
        /// <param name="minutes">Minutes to maintain cached value</param>
        /// <returns>Cached value associated with the specified key</returns>
        /// <remarks>
        /// minutes null - means DefaultCachingMinutes will be used.
        /// minutes 0 - means cached value will be maintained while application running.
        /// </remarks>
        public async Task<T> Get<T>(string key, Func<Task<T>> acquire, uint? minutes)
        {
            // If value is already cached at the specified key, return the value
            if (IsSet(key))
            {
                return Get<T>(key);
            }
            // First execute the function to get the value
            var result = await acquire();
            // Function value will be cached and returned
            Set(key, result, minutes);
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
