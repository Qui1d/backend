using Microsoft.AspNetCore.Mvc;
using SkyVisionStore.BusinessLogic;
using SkyVisionStore.BusinessLogic.Interface;
using SkyVisionStore.Domain.Models.Auth;

namespace SkyVisionStore.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IAuthActions _authActions;

        public AuthController()
        {
            var bl = new SkyVisionStore.BusinessLogic.BusinessLogic();
            _authActions = bl.GetAuthActions();
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

            return Ok(user);
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

            return Created($"/api/user/{user.Id}", user);
        }
    }
}