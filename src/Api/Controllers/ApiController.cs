using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace OwnApt.Api.Controllers
{
    public class ApiController : Controller
    {
        readonly IMemoryCache cache;
        private MemoryCacheEntryOptions cacheOptions;
        private string cacheKeyPrefix;

        public ApiController(IMemoryCache cache)
        {
            this.cache = cache;
            this.cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(1));
            this.cacheKeyPrefix = this.GetType().Name;
        }

        private string CacheKey<TKey>(TKey id) => $"{this.cacheKeyPrefix}:{id}";

        protected void SetCache<TKey, TObj>(TKey id, TObj model)
        {
            this.cache?.Set(this.CacheKey(id), model, this.cacheOptions);
        }

        protected void RemoveCache<TKey>(TKey id)
        {
            this.cache?.Remove(this.CacheKey(id));
        }

        protected bool CheckCache<TKey, TObj>(TKey id, out TObj value) where TObj : class
        {
            if(this.cache == null)
            {
                value = null;
                return false;
            }

            return this.cache.TryGetValue(this.CacheKey(id), out value);
        }
    }
}
