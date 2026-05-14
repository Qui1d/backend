using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkyVisionStore.Api.Services;
using SkyVisionStore.BusinessLogic.Interface;
using SkyVisionStore.Domain.Models.Auth;
using System.Security.Claims;

namespace SkyVisionStore.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthActions _authActions;
        private readonly IUserActions _userActions;
        private readonly JwtTokenService _jwtTokenService;

        public AuthController(JwtTokenService jwtTokenService)
        {
            var bl = new SkyVisionStore.BusinessLogic.BusinessLogic();

            _authActions = bl.GetAuthActions();
            _userActions = bl.GetUserActions();
            _jwtTokenService = jwtTokenService;
        }

        [HttpPost("login")]
        public IActionResult Login([FromBody] UserLoginData loginData)
        {
            if (loginData == null)
            {
                return BadRequest(new { Message = "Login data is required" });
            }

            var user = _authActions.Login(loginData);

            if (user == null)
            {
                return Unauthorized(new { Message = "Invalid email or password" });
            }

            var token = _jwtTokenService.GenerateToken(user);

            return Ok(new
            {
                Token = token,
                User = user
            });
        }

        [HttpPost("register")]
        public IActionResult Register([FromBody] UserRegisterData registerData)
        {
            if (registerData == null)
            {
                return BadRequest(new { Message = "Register data is required" });
            }

            var user = _authActions.Register(registerData);

            if (user == null)
            {
                return BadRequest(new { Message = "User registration failed" });
            }

            var token = _jwtTokenService.GenerateToken(user);

            return Ok(new
            {
                Token = token,
                User = user
            });
        }

        [Authorize]
        [HttpGet("me")]
        public IActionResult GetCurrentUser()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out var userId))
            {
                return Unauthorized(new { Message = "Invalid token" });
            }

            var user = _userActions.GetById(userId);

            if (user == null)
            {
                return NotFound(new { Message = "User not found" });
            }

            return Ok(user);
        }
    }
}