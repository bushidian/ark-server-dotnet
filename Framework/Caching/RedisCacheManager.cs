using System;
using ServiceStack.Redis;
using Microsoft.Extensions.Configuration;

namespace ArkApplication.Framework.Caching
{
     
     public class RedisCacheManager : ICacheManager
     {

        #region Fileds
        
        private readonly RedisManagerPool pool;
         
        #endregion
        
        #region Ctor
        
        public RedisCacheManager(IConfiguration config)
        {

            pool = new RedisManagerPool(config["Cache:Redis"]);
        }
        
        #endregion

        #region Method
        
         /// <summary>
        /// Gets or sets the value associated with the specified key.
        /// </summary>
        /// <typeparam name="T">Type</typeparam>
        /// <param name="key">The key of the value to get.</param>
        /// <returns>The value associated with the specified key.</returns>
        public virtual T Get<T>(string key)
        {
            using (var client = pool.GetClient())
            {
                return client.Get<T>(key);
            }
        }

        /// <summary>
        /// Adds the specified key and object to the cache.
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="data">Data</param>
        /// <param name="cacheTime">Cache time</param>
        public virtual void Set(string key, object data, int cacheTime)
        {
            using(var client = pool.GetClient())
            {
                client.Set(key, data, DateTime.Now.AddMinutes(cacheTime));
            }
        }

        /// <summary>
        /// Gets a value indicating whether the value associated with the specified key is cached
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>Result</returns>
        public bool IsSet(string key)
        {
            using(var client = pool.GetClient())
            {
                return client.ContainsKey(key);
            }
        }

        /// <summary>
        /// Removes the value with the specified key from the cache
        /// </summary>
        /// <param name="key">/key</param>
        public virtual void Remove(string key)
        {
            using(var client = pool.GetClient())
            {
                 client.Remove(key);
            }
        }

        /// <summary>
        /// Removes items by pattern
        /// </summary>
        /// <param name="pattern">pattern</param>
        public virtual void RemoveByPattern(string pattern)
        {
            using(var client = pool.GetClient())
            {
                this.RemoveByPattern(pattern, client.GetAllKeys());
            }
        }

        /// <summary>
        /// Clear all cache data
        /// </summary>
        public virtual void Clear()
        {
            using(var client = pool.GetClient())
            {
                 client.RemoveAll(client.GetAllKeys());
            }
        }

        /// <summary>
        /// Dispose
        /// </summary>
        public virtual void Dispose()
        {
            pool.Dispose();
        }

          
        #endregion

     }
}