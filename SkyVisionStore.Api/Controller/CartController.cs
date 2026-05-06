using Microsoft.AspNetCore.Mvc;
using SkyVisionStore.BusinessLogic;
using SkyVisionStore.BusinessLogic.Interface;
using SkyVisionStore.Domain.Models.Cart;

namespace SkyVisionStore.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CartController : ControllerBase
    {
        private readonly ICartActions _cartActions;

        public CartController()
        {
            var bl = new SkyVisionStore.BusinessLogic.BusinessLogic();
            _cartActions = bl.GetCartActions();
        }

        [HttpGet("{userId}")]
        public IActionResult GetCartByUserId(int userId)
        {
            var cartItems = _cartActions.GetCartByUserId(userId);
            return Ok(cartItems);
        }

        [HttpPost("add")]
        public IActionResult AddToCart([FromBody] AddToCartModel model)
        {
            if (model == null)
            {
                return BadRequest(new { Message = "Cart data is required" });
            }

            var item = _cartActions.AddToCart(model);
            return Ok(item);
        }

        [HttpPut("{userId}/{productId}")]
        public IActionResult UpdateCartItem(int userId, int productId, [FromQuery] int quantity)
        {
            var item = _cartActions.UpdateCartItem(userId, productId, quantity);

            if (item == null)
            {
                return NotFound(new { Message = "Cart item not found" });
            }

            return Ok(item);
        }

        [HttpDelete("{userId}/{productId}")]
        public IActionResult RemoveFromCart(int userId, int productId)
        {
            var deleted = _cartActions.RemoveFromCart(userId, productId);

            if (!deleted)
            {
                return NotFound(new { Message = "Cart item not found" });
            }

            return NoContent();
        }

        [HttpDelete("clear/{userId}")]
        public IActionResult ClearCart(int userId)
        {
            var cleared = _cartActions.ClearCart(userId);

            if (!cleared)
            {
                return NotFound(new { Message = $"Cart for user {userId} not found" });
            }

            return NoContent();
        }
    }
}