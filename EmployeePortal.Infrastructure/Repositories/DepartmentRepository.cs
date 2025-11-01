using EmployeePortal.Application.Interfaces.IRepositories;
using EmployeePortal.Domain.Entities;
using EmployeePortal.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePortal.Infrastructure.Repositories
{
    public class DepartmentRepository : GenericRepository<Department>, IDepartmentRepository
    {
        public DepartmentRepository(EmployeeDbContext context) : base(context)
        {
        }

        public async Task<Department?> GetDepartmentWithEmployeesAsync(int id)
        {
            return await _context.Departments
                .Include(d => d.Employees)
                .FirstOrDefaultAsync(d => d.Id == id);
        }
    }
}
