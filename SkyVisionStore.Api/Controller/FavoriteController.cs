using Microsoft.AspNetCore.Mvc;
using SkyVisionStore.BusinessLogic.Interface;
using SkyVisionStore.Domain.Models.Favorite;

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

        [HttpGet("all")]
        public IActionResult GetAllFavorites()
        {
            return Ok(_favoriteActions.GetAll());
        }

        [HttpGet("{id:int}")]
        public IActionResult GetFavoriteById(int id)
        {
            var favorite = _favoriteActions.GetById(id);

            if (favorite == null)
            {
                return NotFound(new { Message = $"Favorite with ID {id} not found" });
            }

            return Ok(favorite);
        }

        [HttpPost]
        public IActionResult CreateFavorite([FromBody] FavoriteCreateModel favorite)
        {
            var createdFavorite = _favoriteActions.Create(favorite);

            return Created($"/api/favorite/{createdFavorite.Id}", createdFavorite);
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

        [HttpDelete("{id:int}")]
        public IActionResult DeleteFavorite(int id)
        {
            var deleted = _favoriteActions.Delete(id);

            if (!deleted)
            {
                return NotFound(new { Message = $"Favorite with ID {id} not found" });
            }

            return NoContent();
        }

        [HttpGet("user/{userId:int}")]
        public IActionResult GetUserFavorites(int userId)
        {
            var favorites = _favoriteActions.GetFavorites(userId);

            return Ok(favorites);
        }

        [HttpPost("add")]
        public IActionResult AddToFavorites([FromBody] FavoriteCreateModel favorite)
        {
            var addedFavorite = _favoriteActions.AddToFavorites(favorite.UserId, favorite.ProductId);

            if (addedFavorite == null)
            {
                return BadRequest(new { Message = "Product is already in favorites" });
            }

            return Ok(addedFavorite);
        }

        [HttpDelete("remove/user/{userId:int}/product/{productId:int}")]
        public IActionResult RemoveFromFavorites(int userId, int productId)
        {
            var removed = _favoriteActions.RemoveFromFavorites(userId, productId);

            if (!removed)
            {
                return NotFound(new { Message = "Favorite item not found" });
            }

            return NoContent();
        }
    }
}