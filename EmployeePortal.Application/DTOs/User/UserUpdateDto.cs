using EmployeePortal.Domain.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePortal.Application.DTOs.User
{
    public class UserUpdateDto
    {
        [MinLength(5, ErrorMessage = "Password must be at least 5 characters long.")]
        public string? Password { get; set; }

        [Required]
        public UserRole Role { get; set; } = UserRole.User; // ✅ Use enum, not string
    }
}
