using EmployeePortal.Application.DTOs.Auth;
using EmployeePortal.Application.Interfaces.IServices;
using Microsoft.AspNetCore.Mvc;

namespace EmployeePortal.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
            _authService = authService;
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginRequest request)
        {
            var token = await _authService.AuthenticateAsync(request.Username, request.Password);
            if (token == null)
                return Unauthorized("Invalid username or password");

            return Ok(new { token });
        }
    }
}
