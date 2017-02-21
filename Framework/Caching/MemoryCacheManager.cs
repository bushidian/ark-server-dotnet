using System;
using System.Linq;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

namespace ArkApplication.Framework.Caching
{

     /// <summary>
    /// Represents a manager for caching between HTTP requests (long term caching)
    /// </summary>
    public partial class MemoryCacheManager : ICacheManager
    {
        
        #region Filed

        private readonly IMemoryCache cache;
        private List<string> keys = new List<string>();

        #endregion

        #region Constr

        public MemoryCacheManager()
        {
            cache = new MemoryCache(new MemoryCacheOptions());
        }

        #endregion      
  
        /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        public virtual T Get<T>(string key)
        {
            return cache.Get<T>(key);
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">Data</param>
        /// <param name="cacheTime">Cache time</param>
        public virtual void Set(string key, object data, int cacheTime)
        {
            
            cache.Set(key, data, new MemoryCacheEntryOptions()
                 .SetSlidingExpiration(TimeSpan.FromMinutes(cacheTime)));
            if(!keys.Any(o=>o == key))
            {
                keys.Add(key);
            }
          
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>Result</returns>
        public bool IsSet(string key)
        {
            object obj;
            var flag = cache.TryGetValue(key, out obj);
            if(!flag) keys.Remove(key);
            return flag;
        }

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">/key</param>
        public virtual void Remove(string key)
        {
            cache.Remove(key);
            keys.Remove(key);
        }

        /// <summary>
        /// Removes items by pattern
        /// </summary>
        /// <param name="pattern">pattern</param>
        public virtual void RemoveByPattern(string pattern)
        {
            this.RemoveByPattern(pattern, keys);
        }

        /// <summary>
        /// Clear all cache data
        /// </summary>
        public virtual void Clear()
        {
            foreach(var item in keys)
            { 
                this.Remove(item);
            }
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public virtual void Dispose()
        {
            cache.Dispose();
        }

    }

}