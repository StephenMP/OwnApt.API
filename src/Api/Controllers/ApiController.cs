using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Memory;
using OwnApt.Api.Domain.Service;
using System;

namespace OwnApt.Api.Controllers
{
    public class ApiController : Controller
    {
        #region Private Fields

        private readonly IMemoryCacheService cache;
        private readonly string cacheKeyPrefix;
        private MemoryCacheEntryOptions cacheOptions;

        #endregion Private Fields

        #region Public Constructors

        public ApiController(IMemoryCacheService cache)
        {
            this.cache = cache;
            this.cacheOptions = new MemoryCacheEntryOptions().SetAbsoluteExpiration(TimeSpan.FromDays(1));
            this.cacheKeyPrefix = this.GetType().Name;
        }

        #endregion Public Constructors

        #region Protected Methods

        protected bool CheckCache<TKey, TObj>(TKey id, out TObj value) where TObj : class
        {
            if (this.cache == null)
            {
                value = null;
                return false;
            }

            return this.cache.TryGetValue(this.CacheKey(id), out value);
        }

        protected void RemoveCache<TKey>(TKey id)
        {
            this.cache?.Remove(this.CacheKey(id));
        }

        protected void InvalidateCache()
        {
            this.cache?.Invalidate();
        }

        protected void SetCache<TKey, TObj>(TKey id, TObj model)
        {
            this.cache?.Set(this.CacheKey(id), model, this.cacheOptions);
        }

        #endregion Protected Methods

        #region Private Methods

        private string CacheKey<TKey>(TKey id) => $"{this.cacheKeyPrefix}:{id}";

        #endregion Private Methods
    }
}
