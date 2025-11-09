using System.Security.Claims;

namespace EmployeePortal.API.Middleware
{
    public class RequestLoggingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<RequestLoggingMiddleware> _logger;

        public RequestLoggingMiddleware(RequestDelegate next, ILogger<RequestLoggingMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task InvokeAsync(HttpContext context)
        {
            // Ensure user is authenticated before logging
            if (context.User.Identity != null && context.User.Identity.IsAuthenticated)
            {
                var username = context.User.FindFirstValue(ClaimTypes.Name) ?? "UnknownUser";
                var role = context.User.FindFirstValue(ClaimTypes.Role) ?? "NoRole";
                var method = context.Request.Method;
                var path = context.Request.Path;
                var ip = context.Connection.RemoteIpAddress?.ToString() ?? "UnknownIP";

                _logger.LogInformation(
                    "[{Time}] User: {User} (Role: {Role}) | {Method} {Path} | IP: {IP}",
                    DateTime.UtcNow.ToString("yyyy-MM-dd HH:mm:ss"),
                    username,
                    role,
                    method,
                    path,
                    ip
                );
            }

            await _next(context); // Continue request pipeline
        }
    }
}
