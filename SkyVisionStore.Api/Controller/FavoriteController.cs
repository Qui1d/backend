using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkyVisionStore.BusinessLogic.Interface;
using SkyVisionStore.Domain.Models.Favorite;
using System.Security.Claims;

namespace SkyVisionStore.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class FavoriteController : ControllerBase
    {
        private readonly IFavoriteActions _favoriteActions;

        public FavoriteController()
        {
            var bl = new SkyVisionStore.BusinessLogic.BusinessLogic();
            _favoriteActions = bl.GetFavoriteActions();
        }

        [Authorize]
        [HttpGet]
        public IActionResult GetMyFavorites()
        {
            var userId = GetCurrentUserId();

            if (userId == null)
            {
                return Unauthorized(new { Message = "Invalid token" });
            }

            var favorites = _favoriteActions.GetFavorites(userId.Value);

            return Ok(favorites);
        }

        [Authorize]
        [HttpPost("{productId:int}")]
        public IActionResult AddToFavorites(int productId)
        {
            var userId = GetCurrentUserId();

            if (userId == null)
            {
                return Unauthorized(new { Message = "Invalid token" });
            }

            var favorite = _favoriteActions.AddToFavorites(userId.Value, productId);

            if (favorite == null)
            {
                return BadRequest(new { Message = "User or product not found" });
            }

            return Ok(favorite);
        }

        [Authorize]
        [HttpDelete("{productId:int}")]
        public IActionResult RemoveFromFavorites(int productId)
        {
            var userId = GetCurrentUserId();

            if (userId == null)
            {
                return Unauthorized(new { Message = "Invalid token" });
            }

            var removed = _favoriteActions.RemoveFromFavorites(userId.Value, productId);

            if (!removed)
            {
                return NotFound(new { Message = "Favorite item not found" });
            }

            return NoContent();
        }

        [HttpGet("all")]
        public IActionResult GetAllFavorites()
        {
            return Ok(_favoriteActions.GetAll());
        }

        [HttpGet("id/{id:int}")]
        public IActionResult GetFavoriteById(int id)
        {
            var favorite = _favoriteActions.GetById(id);

            if (favorite == null)
            {
                return NotFound(new { Message = $"Favorite with ID {id} not found" });
            }

            return Ok(favorite);
        }

        [HttpPost("create")]
        public IActionResult CreateFavorite([FromBody] FavoriteCreateModel favorite)
        {
            var createdFavorite = _favoriteActions.Create(favorite);

            return Created($"/api/favorite/id/{createdFavorite.Id}", createdFavorite);
        }

        [HttpPut("{id:int}")]
        public IActionResult UpdateFavorite(int id, [FromBody] FavoriteUpdateModel updatedFavorite)
        {
            var favorite = _favoriteActions.Update(id, updatedFavorite);

            if (favorite == null)
            {
                return NotFound(new { Message = $"Favorite with ID {id} not found" });
            }

            return Ok(favorite);
        }

        [HttpDelete("id/{id:int}")]
        public IActionResult DeleteFavorite(int id)
        {
            var deleted = _favoriteActions.Delete(id);

            if (!deleted)
            {
                return NotFound(new { Message = $"Favorite with ID {id} not found" });
            }

            return NoContent();
        }

        private int? GetCurrentUserId()
        {
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out var userId))
            {
                return null;
            }

            return userId;
        }
    }
}