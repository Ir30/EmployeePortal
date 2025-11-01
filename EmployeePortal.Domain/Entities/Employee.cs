using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePortal.Domain.Entities
{
    public class Employee
    {
        public int EmployeeId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string? Email { get; set; }
        public int DepartmentId { get; set; }

        // Navigation property
        public Department? Department { get; set; }
    }
}
