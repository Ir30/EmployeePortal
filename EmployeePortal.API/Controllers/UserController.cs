using EmployeePortal.Application.DTOs.User;
using EmployeePortal.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace EmployeePortal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize] // ✅ All actions require authentication by default
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        // ✅ GET: api/user
        // Only Admin can list all users
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            var users = await _userService.GetAllUsersAsync();
            return Ok(users);
        }

        // ✅ GET: api/user/{id}
        // Admin can view any user, others can view only their own data
        [HttpGet("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetById(int id)
        {
            var currentUsername = User.FindFirstValue(ClaimTypes.Name);
            var currentRole = User.FindFirstValue(ClaimTypes.Role);

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            // Non-admins can only access their own profile
            if (currentRole != "Admin" && user.Username != currentUsername)
                return Forbid();

            return Ok(user);
        }

        // ✅ GET: api/user/me
        // Any logged-in user can view their own profile
        [HttpGet("me")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> GetMyProfile()
        {
            var username = User.FindFirstValue(ClaimTypes.Name);
            if (string.IsNullOrEmpty(username))
                return Unauthorized();

            var user = await _userService.GetUserByUsernameAsync(username);
            if (user == null)
                return NotFound("User not found");

            return Ok(user);
        }

        // ✅ POST: api/user
        // Only Admin can create new users
        [HttpPost]
        [AllowAnonymous]
        public async Task<IActionResult> Create([FromBody] UserCreateDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            try
            {
                var user = await _userService.AddUserAsync(dto);
                return CreatedAtAction(nameof(GetById), new { id = user.UserId }, user);
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }

        // ✅ PUT: api/user/{id}
        // Admin can edit anyone; users can only edit their own account
        [HttpPut("{id}")]
        [Authorize(Roles = "Admin,User")]
        public async Task<IActionResult> Update(int id, [FromBody] UserUpdateDto dto)
        {
            var currentUsername = User.FindFirstValue(ClaimTypes.Name);
            var currentRole = User.FindFirstValue(ClaimTypes.Role);

            var user = await _userService.GetUserByIdAsync(id);
            if (user == null)
                return NotFound();

            if (currentRole != "Admin" && user.Username != currentUsername)
                return Forbid();

            await _userService.UpdateUserAsync(id, dto);
            return NoContent();
        }

        // ✅ DELETE: api/user/{id}
        // Only Admin can delete users
        [HttpDelete("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var existing = await _userService.GetUserByIdAsync(id);
            if (existing == null)
                return NotFound();

            await _userService.DeleteUserAsync(id);
            return NoContent();
        }
    }
}
