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

        public ApiController(IMemoryCache cache)
        {
            this.cache = cache;
            this.cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(1));
        }

        protected void SetCache<TObj>(object key, TObj model) where TObj : class
        {
            this.cache?.Set(key, model, this.cacheOptions);
        }

        protected void RemoveCache(object key)
        {
            this.cache?.Remove(key);
        }

        protected bool CheckCache<TObj>(object key, out TObj value) where TObj : class
        {
            if(this.cache == null)
            {
                value = null;
                return false;
            }

            return this.cache.TryGetValue(key, out value);
        }
    }
}
