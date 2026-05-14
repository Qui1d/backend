using Microsoft.AspNetCore.Mvc;
using SkyVisionStore.BusinessLogic.Interface;
using SkyVisionStore.Domain.Entities.Cart;
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

            var response = cartItems.Select(MapCartItem).ToList();

            return Ok(response);
        }

        [HttpPost("add")]
        public IActionResult AddToCart([FromBody] AddToCartModel model)
        {
            if (model == null)
            {
                return BadRequest(new { Message = "Cart data is required" });
            }

            var item = _cartActions.AddToCart(model);

            if (item == null)
            {
                return BadRequest(new { Message = "User or product not found" });
            }

            return Ok(MapCartItem(item));
        }

        [HttpPut("{userId}/{productId}")]
        public IActionResult UpdateCartItem(int userId, int productId, [FromQuery] int quantity)
        {
            var item = _cartActions.UpdateCartItem(userId, productId, quantity);

            if (item == null)
            {
                return NotFound(new { Message = "Cart item not found" });
            }

            return Ok(MapCartItem(item));
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
                return NoContent();
            }

            return NoContent();
        }

        private static object MapCartItem(CartItem item)
        {
            return new
            {
                item.Id,
                item.UserId,
                item.ProductId,
                item.Quantity,
                item.AddedAt,

                Product = new
                {
                    item.Product.Id,
                    item.Product.Title,
                    item.Product.Slug,
                    item.Product.Platform,
                    item.Product.Genre,
                    item.Product.Price,
                    item.Product.OldPrice,
                    item.Product.Discount,
                    item.Product.Image,
                    item.Product.RecommendedImage,
                    item.Product.Region,
                    item.Product.Description,
                    item.Product.Requirements,
                    item.Product.IsNew,
                    item.Product.IsPopular,
                    item.Product.IsUpcoming,
                    item.Product.CreatedAt
                }
            };
        }
    }
}