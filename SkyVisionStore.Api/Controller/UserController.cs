using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkyVisionStore.BusinessLogic.Interface;
using SkyVisionStore.Domain.Models.User;

namespace SkyVisionStore.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserActions _userActions;

        public UserController()
        {
            var bl = new SkyVisionStore.BusinessLogic.BusinessLogic();
            _userActions = bl.GetUserActions();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public IActionResult GetAllUsers()
        {
            return Ok(_userActions.GetAll());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userActions.GetById(id);

            if (user == null)
            {
                return NotFound(new { Message = $"User with ID {id} not found" });
            }

            return Ok(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public IActionResult CreateUser([FromBody] UserCreateModel user)
        {
            var createdUser = _userActions.Create(user);

            return Created($"/api/user/{createdUser.Id}", createdUser);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] UserUpdateModel updatedUser)
        {
            var user = _userActions.Update(id, updatedUser);

            if (user == null)
            {
                return NotFound(new { Message = $"User with ID {id} not found" });
            }

            return Ok(user);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var deleted = _userActions.Delete(id);

            if (!deleted)
            {
                return NotFound(new { Message = $"User with ID {id} not found" });
            }

            return NoContent();
        }
    }
}