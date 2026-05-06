using Microsoft.AspNetCore.Mvc;
using SkyVisionStore.BusinessLogic;
using SkyVisionStore.BusinessLogic.Interface;

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

        [HttpGet("{userId}")]
        public IActionResult GetFavorites(int userId)
        {
            var favorites = _favoriteActions.GetFavorites(userId);
            return Ok(favorites);
        }

        [HttpPost("{userId}/{productId}")]
        public IActionResult AddToFavorites(int userId, int productId)
        {
            var favorite = _favoriteActions.AddToFavorites(userId, productId);
            return Ok(favorite);
        }

        [HttpDelete("{userId}/{productId}")]
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