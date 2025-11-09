using EmployeePortal.Application.DTOs.User;
using EmployeePortal.Application.Interfaces.IRepositories;
using EmployeePortal.Application.Interfaces.IServices;
using EmployeePortal.Domain.Entities;
using EmployeePortal.Domain.Enums;
using System.Security.Cryptography;
using System.Text;

namespace EmployeePortal.Application.Services
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        // ✅ Get all users (Admin-only)
        public async Task<IEnumerable<User>> GetAllUsersAsync()
        {
            return await _userRepository.GetAllAsync();
        }

        // ✅ Get by ID
        public async Task<User?> GetUserByIdAsync(int id)
        {
            return await _userRepository.GetByIdAsync(id);
        }

        // ✅ Get by username
        public async Task<User?> GetUserByUsernameAsync(string username)
        {
            return await _userRepository.GetByUsernameAsync(username);
        }

        // ✅ Create new user
        public async Task<User> AddUserAsync(UserCreateDto dto)
        {
            // Prevent duplicate usernames
            var existing = await _userRepository.GetByUsernameAsync(dto.Username);
            if (existing != null)
                throw new Exception("Username already exists");

            // Validate role (only Admin or User allowed)
            if (!Enum.IsDefined(typeof(UserRole), dto.Role))
                throw new Exception("Invalid role. Allowed roles are 'Admin' and 'User'.");

            var hashedPassword = HashPassword(dto.Password);

            var user = new User
            {
                Username = dto.Username,
                PasswordHash = hashedPassword,
                Role = dto.Role // Enum is directly mapped
            };

            await _userRepository.AddAsync(user);
            await _userRepository.SaveAsync();

            return user;
        }

        // ✅ Update user details
        public async Task UpdateUserAsync(int id, UserUpdateDto dto)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new Exception("User not found");

            // Update password if provided
            if (!string.IsNullOrWhiteSpace(dto.Password))
                user.PasswordHash = HashPassword(dto.Password);

            // Validate role (only Admin or User)
            if (!Enum.IsDefined(typeof(UserRole), dto.Role))
                throw new Exception("Invalid role. Allowed roles are 'Admin' and 'User'.");

            user.Role = dto.Role;

            _userRepository.Update(user);
            await _userRepository.SaveAsync();
        }


        // ✅ Delete user by ID
        public async Task DeleteUserAsync(int id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            if (user == null)
                throw new Exception("User not found");

            _userRepository.Delete(user);
            await _userRepository.SaveAsync();
        }

        // 🔒 Hash password securely
        private string HashPassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password))
                throw new ArgumentException("Password cannot be empty");

            using var sha256 = SHA256.Create();
            var bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
            return BitConverter.ToString(bytes).Replace("-", "").ToLower();
        }
    }
}
