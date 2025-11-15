using System.Collections.Concurrent;

namespace EmployeePortal.Infrastructure.Singletons
{
    public sealed class CacheManager
    {
        private static readonly Lazy<CacheManager> _instance =
            new Lazy<CacheManager>(() => new CacheManager());

        public static CacheManager Instance => _instance.Value;

        private readonly ConcurrentDictionary<string, object> _cache =
            new ConcurrentDictionary<string, object>();

        private CacheManager() { }

        public void Set(string key, object value)
        {
            _cache[key] = value;
        }

        public T? Get<T>(string key)
        {
            if (_cache.TryGetValue(key, out var value))
                return (T)value;

            return default;
        }

        public void Remove(string key)
        {
            _cache.TryRemove(key, out _);
        }
    }
}
