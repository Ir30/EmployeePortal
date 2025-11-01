using EmployeePortal.Application.Interfaces.IRepositories;
using EmployeePortal.Application.Interfaces.IServices;
using EmployeePortal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePortal.Application.Services
{
    public class DepartmentService : IDepartmentService
    {
        private readonly IDepartmentRepository _departmentRepository;

        public DepartmentService(IDepartmentRepository departmentRepository)
        {
            _departmentRepository = departmentRepository;
        }

        public async Task<IEnumerable<Department>> GetAllDepartmentsAsync()
        {
            return await _departmentRepository.GetAllAsync();
        }

        public async Task<Department?> GetDepartmentByIdAsync(int id)
        {
            return await _departmentRepository.GetDepartmentWithEmployeesAsync(id);
        }

        public async Task AddDepartmentAsync(Department department)
        {
            await _departmentRepository.AddAsync(department);
            await _departmentRepository.SaveAsync();
        }

        public async Task UpdateDepartmentAsync(Department department)
        {
            _departmentRepository.Update(department);
            await _departmentRepository.SaveAsync();
        }

        public async Task DeleteDepartmentAsync(int id)
        {
            var dept = await _departmentRepository.GetByIdAsync(id);
            if (dept != null)
            {
                _departmentRepository.Delete(dept);
                await _departmentRepository.SaveAsync();
            }
        }
    }

}
