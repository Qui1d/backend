using Microsoft.AspNetCore.Mvc;
using SkyVisionStore.BusinessLogic;
using SkyVisionStore.BusinessLogic.Interfaces;
using SkyVisionStore.Domain.Entities.User;

namespace SkyVisionStore.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserBL _userBL;

        public UserController()
        {
            var bl = new BusinessLogic();
            _userBL = bl.GetUserBL();
        }

        [HttpGet("all")]
        public IActionResult GetAllUsers()
        {
            return Ok(_userBL.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetUserById(int id)
        {
            var user = _userBL.GetById(id);

            if (user == null)
            {
                return NotFound(new { Message = $"User with ID {id} not found" });
            }

            return Ok(user);
        }

        [HttpPost]
        public IActionResult CreateUser([FromBody] User user)
        {
            var createdUser = _userBL.Create(user);
            return Created($"/api/user/{createdUser.Id}", createdUser);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateUser(int id, [FromBody] User updatedUser)
        {
            var user = _userBL.Update(id, updatedUser);

            if (user == null)
            {
                return NotFound(new { Message = $"User with ID {id} not found" });
            }

            return Ok(user);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteUser(int id)
        {
            var deleted = _userBL.Delete(id);

            if (!deleted)
            {
                return NotFound(new { Message = $"User with ID {id} not found" });
            }

            return NoContent();
        }
    }
}