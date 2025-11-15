
namespace EmployeePortal.Application.Interfaces.IServices
{
    public interface ICacheService
    {
        void Set(string key, object value);
        T? Get<T>(string key);
        void Remove(string key);
    }
}
