using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;

namespace OwnApt.Api.Domain.Service
{
    public interface IMemoryCacheService : IDisposable
    {
        #region Public Methods

        void Invalidate();

        void Remove(object key);

        void Set<TItem>(object key, TItem value, MemoryCacheEntryOptions options);

        bool TryGetValue<TItem>(object key, out TItem value) where TItem : class;

        #endregion Public Methods
    }

    public class MemoryCacheService : IMemoryCacheService
    {
        #region Private Fields

        private readonly IList<object> cacheKeys;
        private readonly IMemoryCache memoryCache;
        private bool disposedValue;

        #endregion Private Fields

        #region Public Constructors

        public MemoryCacheService(IMemoryCache memoryCache)
        {
            this.memoryCache = memoryCache;
            this.cacheKeys = new List<object>();
        }

        #endregion Public Constructors

        #region Public Methods

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public void Invalidate()
        {
            foreach (var key in this.cacheKeys)
            {
                this.memoryCache.Remove(key);
            }

            this.cacheKeys.Clear();
        }

        public void Remove(object key)
        {
            this.memoryCache.Remove(key);
            this.cacheKeys.Remove(key);
        }

        public void Set<TItem>(object key, TItem value, MemoryCacheEntryOptions options)
        {
            this.memoryCache.Set(key, value, options);
            this.cacheKeys.Add(key);
        }

        public bool TryGetValue<TItem>(object key, out TItem value) where TItem : class
        {
            return this.memoryCache.TryGetValue(key, out value);
        }

        #endregion Public Methods

        #region Protected Methods

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    this.memoryCache?.Dispose();
                }

                disposedValue = true;
            }
        }

        #endregion Protected Methods
    }
}
