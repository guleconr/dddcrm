using Microsoft.Extensions.Caching.Memory;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;
using TBBProject.Core.Common;
using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading;

namespace TBBProject.Core.Caching
{
    public sealed class InMemoryCacheProvider : ICacheProvider
    {
        private static CancellationTokenSource _resetCacheToken = new CancellationTokenSource();
        private readonly IMemoryCache _cache;
        private readonly ConcurrentDictionary<string, bool> _keys;
        private readonly IOptions<InMemoryCacheOptions> _options;

        public InMemoryCacheProvider(
                                     IMemoryCache cache,
                                     IOptions<InMemoryCacheOptions> options
                                    )
        {
            _cache = cache;
            _keys = new ConcurrentDictionary<string, bool>();
            _options = options;
        }

        private int ExpirationSeconds => _options.Value.ExpirationInSeconds;

        public void Set<T>(string key, T value)
            where T : class
        {
            if (IsValid(key, value))
            {
                Set(key, value, TimeSpan.FromSeconds(ExpirationSeconds));
            }
        }

        public void Set<T>(string key, T value, TimeSpan expiration)
            where T : class
        {
            if (IsValid(key, value))
            {
                _cache.Set(key, value, GetMemoryCacheEntryOptions(expiration));
                _keys.TryAdd(key, true);
            }
        }

        public T Get<T>(string key)
            where T : class
        {
            if (IsValid(key))
            {
                if (_cache.Get(key) is T result)
                {
                    return result;
                }
            }
            return default;
        }

        public T Get<T>(string key, Func<T> acquire)
            where T : class
        {
            if (IsValid(key))
            {
                if (_cache.TryGetValue(key, out T value))
                {
                    if (value != null)
                    {
                        return value;
                    }
                }

                var result = acquire();
                
                _cache.Set(key, result, GetMemoryCacheEntryOptions(TimeSpan.FromSeconds(ExpirationSeconds)));
                _keys.TryAdd(key, true);

                return result;
            }
            else
            {
                return null;
            }
        }

        public void Remove(string key)
        {
            if (IsValid(key))
            {
                _cache.Remove(key);
                _keys.TryGetValue(key, out _);
            }
        }

        public void RemovePattern(string pattern)
        {

            var matchesKeys = _keys.Select(p => p.Key).Where(key => key.StartsWith(pattern)).ToList();

            foreach (var key in matchesKeys)
            {
                Remove(key);
            }
        }

        public void RemovePatternExist(string pattern)
        {

            var matchesKeys = _keys.Select(p => p.Key).Where(key => key.Contains(pattern)).ToList();

            foreach (var key in matchesKeys)
            {
                Remove(key);
            }
        }

        public bool Exists(string key)
        {
            var value = false;

            if (IsValid(key))
            {
                value = _cache.TryGetValue(key, out _);
            }
            return value;
        }

        public void Refresh<T>(string key, T value)
            where T : class
        {
            if (IsValid(key, value))
            {
                Remove(key);
                Set(key, value, TimeSpan.FromSeconds(ExpirationSeconds));
            }
        }

        public void Refresh<T>(string key, T value, TimeSpan expiration)
            where T : class
        {
            if (IsValid(key, value))
            {
                Remove(key);
                Set(key, value, expiration);
            }
        }

        public void Flush()
        {
            if (_resetCacheToken != null && _resetCacheToken.IsCancellationRequested && _resetCacheToken.Token.CanBeCanceled)
            {
                _resetCacheToken.Cancel();
                _resetCacheToken.Dispose();
            }

            _resetCacheToken = new CancellationTokenSource();
        }

        private void PostEviction(object key, object value, EvictionReason reason, object state)
        {
            if (reason == EvictionReason.Replaced)
            {
                return;
            }

            ClearKeys();

            TryRemoveKey(key.ToString());
        }

        private void ClearKeys()
        {
            foreach (var key in _keys.Where(p => !p.Value).Select(p => p.Key).ToList())
            {
                RemoveKey(key);
            }
        }

        private string RemoveKey(string key)
        {
            if (IsValid(key))
            {
                TryRemoveKey(key);
            }
            return key;
        }

        private void TryRemoveKey(string key)
        {
            if (IsValid(key))
            {
                if (!_keys.TryRemove(key, out _))
                {
                    _keys.TryUpdate(key, false, true);
                }
            }
        }

        private MemoryCacheEntryOptions GetMemoryCacheEntryOptions(TimeSpan cacheTime)
        {
            var options = new MemoryCacheEntryOptions()
                .AddExpirationToken(new CancellationChangeToken(_resetCacheToken.Token))
                .RegisterPostEvictionCallback(PostEviction);

            options.AbsoluteExpirationRelativeToNow = cacheTime;

            return options;
        }

        public bool IsValid(string key, object value)
        {
            var logger = TBBProjectContext.Current.Resolve<ILogger<InMemoryCacheProvider>>();

            if (string.IsNullOrEmpty(key) && value != null)
            {
                return false;
            }
            if (string.IsNullOrEmpty(key) == false && value == null)
            {
                return true;
            }
            else
            {
                return true;
            }
        }

        public bool IsValid(string key)
        {
            var logger = TBBProjectContext.Current.Resolve<ILogger<InMemoryCacheProvider>>();

            if (string.IsNullOrEmpty(key))
            {
                return false;
            }
            else
            {
                return true;
            }
        }
    }
}
