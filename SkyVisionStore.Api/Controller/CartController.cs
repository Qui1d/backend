using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkyVisionStore.BusinessLogic.Interface;
using SkyVisionStore.Domain.Entities.Cart;
using System.Security.Claims;

namespace SkyVisionStore.Api.Controller
{
    [Authorize]
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

        [HttpGet]
        public IActionResult GetCart()
        {
            var userId = GetCurrentUserId();

            if (userId == null)
            {
                return Unauthorized(new { Message = "Invalid token" });
            }

            var cartItems = _cartActions.GetCartByUserId(userId.Value);

            return Ok(cartItems.Select(MapCartItem));
        }

        [HttpPost("add")]
        public IActionResult AddToCart([FromBody] AddCartItemRequest model)
        {
            var userId = GetCurrentUserId();

            if (userId == null)
            {
                return Unauthorized(new { Message = "Invalid token" });
            }

            if (model == null)
            {
                return BadRequest(new { Message = "Cart data is required" });
            }

            var item = _cartActions.AddToCart(
                userId.Value,
                model.ProductId,
                model.Quantity
            );

            if (item == null)
            {
                return BadRequest(new { Message = "User or product not found" });
            }

            return Ok(MapCartItem(item));
        }

        [HttpPut("{productId}")]
        public IActionResult UpdateCartItem(int productId, [FromQuery] int quantity)
        {
            var userId = GetCurrentUserId();

            if (userId == null)
            {
                return Unauthorized(new { Message = "Invalid token" });
            }

            var item = _cartActions.UpdateCartItem(
                userId.Value,
                productId,
                quantity
            );

            if (item == null)
            {
                return NotFound(new { Message = "Cart item not found" });
            }

            return Ok(MapCartItem(item));
        }

        [HttpDelete("{productId}")]
        public IActionResult RemoveFromCart(int productId)
        {
            var userId = GetCurrentUserId();

            if (userId == null)
            {
                return Unauthorized(new { Message = "Invalid token" });
            }

            var deleted = _cartActions.RemoveFromCart(userId.Value, productId);

            if (!deleted)
            {
                return NotFound(new { Message = "Cart item not found" });
            }

            return NoContent();
        }

        [HttpDelete("clear")]
        public IActionResult ClearCart()
        {
            var userId = GetCurrentUserId();

            if (userId == null)
            {
                return Unauthorized(new { Message = "Invalid token" });
            }

            _cartActions.ClearCart(userId.Value);

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

        private static object MapCartItem(CartItem item)
        {
            return new
            {
                item.Id,
                item.UserId,
                item.ProductId,
                item.Quantity,
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

    public class AddCartItemRequest
    {
        public int ProductId { get; set; }

        public int Quantity { get; set; }
    }
}