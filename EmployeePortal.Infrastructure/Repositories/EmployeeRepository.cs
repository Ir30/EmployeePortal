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
    public class EmployeeRepository : GenericRepository<Employee>, IEmployeeRepository
    {
        public EmployeeRepository(EmployeeDbContext context) : base(context)
        {
        }

        public async Task<IEnumerable<Employee>> GetEmployeesWithDepartmentAsync()
        {
            return await _context.Employees
                .Include(e => e.Department)
                .ToListAsync();
        }
    }
}
