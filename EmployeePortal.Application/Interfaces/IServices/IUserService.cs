using EmployeePortal.Application.DTOs.User;
using EmployeePortal.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeePortal.Application.Interfaces.IServices
{
    public interface IUserService
    {
        Task<IEnumerable<User>> GetAllUsersAsync();
        Task<User?> GetUserByIdAsync(int id);
        Task<User?> GetUserByUsernameAsync(string username);
        Task<User> AddUserAsync(UserCreateDto dto);
        Task UpdateUserAsync(int id, UserUpdateDto dto);
        Task DeleteUserAsync(int id);
    }
}
