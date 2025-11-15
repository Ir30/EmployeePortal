using EmployeePortal.Application.Interfaces.IServices;
using EmployeePortal.Infrastructure.Singletons;

namespace EmployeePortal.Infrastructure.Services
{
    public class CacheService : ICacheService
    {
        public void Set(string key, object value)
        {
            CacheManager.Instance.Set(key, value);
        }

        public T? Get<T>(string key)
        {
            return CacheManager.Instance.Get<T>(key);
        }

        public void Remove(string key)
        {
            CacheManager.Instance.Remove(key);
        }
    }
}
