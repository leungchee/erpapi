
using ERPAPI.Services;
using Microsoft.AspNetCore.Mvc;

namespace ERPAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController : ControllerBase
    {
        private readonly JwtService _jwtService;
        private readonly PasswordService _passwordService;
        public AuthController(JwtService jwtService, PasswordService passwordService)
        {
            _passwordService= passwordService;
            _jwtService = jwtService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] LoginRequest request)
        {
            // 这里简化了认证逻辑，实际应用中应该验证用户名和密码
            if (request.Username == "admin" && _passwordService.VerifyPassword("admin123",request.Password))
            {
                var token = _jwtService.GenerateToken(request.Username);
                return Ok(new { token });
            }

            return Unauthorized();
        }
    }

    public class LoginRequest
    {
        public string Username { get; set; } = string.Empty;
        public string Password { get; set; } = string.Empty;
    }
} 