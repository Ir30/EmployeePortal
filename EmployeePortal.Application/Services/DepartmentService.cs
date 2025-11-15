using EmployeePortal.Application.Interfaces.IRepositories;
using EmployeePortal.Application.Interfaces.IServices;
using EmployeePortal.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeePortal.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;
        private readonly ICacheService _cacheService;

        private const string CACHE_KEY = "departments";

        public DepartmentService(
            IDepartmentRepository departmentRepository,
            ICacheService cacheService)
        {
            _departmentRepository = departmentRepository;
            _cacheService = cacheService;
        }

        // 🚀 GET ALL (WITH CACHE)
        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            // Check cache
            var cached = _cacheService.Get<IEnumerable<Department>>(CACHE_KEY);
            if (cached != null)
                return cached;

            // Fetch from DB
            var departments = await _departmentRepository.GetAllAsync();

            // Store in cache
            _cacheService.Set(CACHE_KEY, departments);

            return departments;
        }

        // 🚀 GET BY ID (NO CACHE — safer for detailed/related data)
        public async Task<Department?> GetDepartmentByIdAsync(int id)
        {
            return await _departmentRepository.GetDepartmentWithEmployeesAsync(id);
        }

        // 🚀 ADD (CLEAR CACHE)
        public async Task AddDepartmentAsync(Department department)
        {
            await _departmentRepository.AddAsync(department);
            await _departmentRepository.SaveAsync();

            // Invalidate cache
            _cacheService.Remove(CACHE_KEY);
        }

        // 🚀 UPDATE (CLEAR CACHE)
        public async Task UpdateDepartmentAsync(Department department)
        {
            _departmentRepository.Update(department);
            await _departmentRepository.SaveAsync();

            // Invalidate cache
            _cacheService.Remove(CACHE_KEY);
        }

        // 🚀 DELETE (CLEAR CACHE)
        public async Task DeleteDepartmentAsync(int id)
        {
            var dept = await _departmentRepository.GetByIdAsync(id);
            if (dept != null)
            {
                _departmentRepository.Delete(dept);
                await _departmentRepository.SaveAsync();
            }

            // Invalidate cache
            _cacheService.Remove(CACHE_KEY);
        }
    }
}
