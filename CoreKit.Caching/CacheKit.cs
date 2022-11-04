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
        /// <remarks>No cache</remarks>
        public CacheKit() : this(new CacheKitConfiguration { }) { }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">Configuration <see cref="CacheKitConfiguration"/></param>
        public CacheKit(IOptions<CacheKitConfiguration> configuration) : this(configuration.Value) { }

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

            // ...
            return Cache.Contains(key);

        }

        /// <summary>
        /// Adds the specified key and value to the cache.
        /// </summary>
        /// <remarks>DefaultCachingMinutes will be used</remarks>
        /// <param name="key">Key of the value to cache</param>
        /// <param name="data">Value of object to be cached</param>
        public void Set(string key, object data)
        {

            // Actually set ...
            Set(key, data, Configuration.DefaultCachingMinutes);

        }

        /// <summary>
        /// Adds the specified key and value to the cache
        /// </summary>
        /// <param name="key">Key of the value to cache</param>
        /// <param name="data">value of object to be cached</param>
        /// <param name="minutes">Minutes to maintain cached value</param>
        /// <remarks>
        /// Minutes 0 - means no cache will be used.
        /// </remarks>
        public void Set(string key, object data, uint minutes)
        {

            // In case the data is not present, nothing will be cached
            if (data == null)
            {
                return;
            }

            // Removing existing value of given key
            Cache.Remove(key);

            // Defining expiration
            var expiration = DateTime.Now + TimeSpan.FromMinutes(minutes);

            // Caching the data with composed policy
            Cache.Add(
                new CacheItem(key, data),
                new CacheItemPolicy { AbsoluteExpiration = expiration }
            );

            // policy.Priority = CacheItemPriority.NotRemovable;
        }

        /// <summary>
        /// Get a cached value
        /// </summary>
        /// <typeparam name="T">Type of the chached value</typeparam>
        /// <param name="key">Key of the chached value</param>
        /// <returns>Cached value associated with the specified key</returns>
        public T Get<T>(string key)
        {

            // ...
            return (T)Cache[key];

        }

        /// <summary>
        /// Get a cached value. If it's not there, first load and cache it.
        /// </summary>
        /// <remarks>DefaultCachingMinutes will be used</remarks>
        /// <typeparam name="T">Type of the chached value</typeparam>
        /// <param name="key">Key of the chached value</param>
        /// <param name="acquire">Function to load value if it's not in the cache yet</param>
        /// <returns>Cached value associated with the specified key</returns>
        public async Task<T> Get<T>(string key, Func<Task<T>> acquire)
        {

            // Get the cached value
            return await Get(key, acquire, Configuration.DefaultCachingMinutes);

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
        /// Minutes 0 - means no cache will be used.
        /// </remarks>
        public async Task<T> Get<T>(string key, Func<Task<T>> acquire, uint minutes)
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
        /// Get a cached value. If it's not there, first load and cache it. (DefaultCachingMinutes will be used)
        /// </summary>
        /// <typeparam name="T">Type of the chached value</typeparam>
        /// <param name="key">Key of the chached value</param>
        /// <param name="acquire">Function to load value if it's not in the cache yet</param>
        /// <returns>Cached value associated with the specified key</returns>
        public T Get<T>(string key, Func<T> acquire)
        {

            // Get the cached value
            return Get(key, acquire, Configuration.DefaultCachingMinutes);

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
        /// Minutes 0 - means no cache will be used.
        /// </remarks>
        public T Get<T>(string key, Func<T> acquire, uint minutes)
        {

            // If value is already cached at the specified key, return the value
            if (IsSet(key))
            {
                return Get<T>(key);
            }

            // First execute the function to get the value
            var result = acquire();

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

            // remove every item from the cache
            foreach (var item in Cache)
            {
                Remove(item.Key);
            }

        }

    }

}
