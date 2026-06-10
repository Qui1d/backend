using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkyVisionStore.BusinessLogic.Interface;
using SkyVisionStore.Domain.Enums;
using SkyVisionStore.Domain.Models.Order;

namespace SkyVisionStore.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderActions _orderActions;

        public OrderController()
        {
            var bl = new SkyVisionStore.BusinessLogic.BusinessLogic();
            _orderActions = bl.GetOrderActions();
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("all")]
        public IActionResult GetAllOrders()
        {
            return Ok(_orderActions.GetAll());
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("user/{userId:int}")]
        public IActionResult GetOrdersByUserId(int userId)
        {
            return Ok(_orderActions.GetOrdersByUserId(userId));
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("{orderId:int}")]
        public IActionResult GetOrderById(int orderId)
        {
            var order = _orderActions.GetOrderById(orderId);

            if (order == null)
            {
                return NotFound(new { Message = $"Order with ID {orderId} not found" });
            }

            return Ok(order);
        }

        [Authorize]
        [HttpGet("my")]
        public IActionResult GetMyOrders()
        {
            var userId = GetCurrentUserId();

            if (userId == null)
            {
                return Unauthorized(new { Message = "User ID was not found in token" });
            }

            var orders = _orderActions.GetOrdersByUserId(userId.Value);

            return Ok(orders);
        }

        [Authorize]
        [HttpPost("checkout")]
        public IActionResult Checkout()
        {
            var userId = GetCurrentUserId();

            if (userId == null)
            {
                return Unauthorized(new { Message = "User ID was not found in token" });
            }

            var order = _orderActions.CheckoutFromCart(userId.Value);

            if (order == null)
            {
                return BadRequest(new { Message = "Cart is empty" });
            }

            return Ok(order);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public IActionResult CreateOrder([FromBody] CreateOrderModel model)
        {
            var order = _orderActions.CreateOrder(model);

            return Created($"/api/order/{order.Id}", order);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{orderId:int}")]
        public IActionResult UpdateOrder(int orderId, [FromBody] OrderUpdateModel model)
        {
            var order = _orderActions.UpdateOrder(orderId, model);

            if (order == null)
            {
                return NotFound(new { Message = $"Order with ID {orderId} not found" });
            }

            return Ok(order);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{orderId:int}/status")]
        public IActionResult UpdateOrderStatus(int orderId, [FromQuery] OrderStatus status)
        {
            var order = _orderActions.UpdateOrderStatus(orderId, status);

            if (order == null)
            {
                return NotFound(new { Message = $"Order with ID {orderId} not found" });
            }

            return Ok(order);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{orderId:int}")]
        public IActionResult DeleteOrder(int orderId)
        {
            var deleted = _orderActions.DeleteOrder(orderId);

            if (!deleted)
            {
                return NotFound(new { Message = $"Order with ID {orderId} not found" });
            }

            return NoContent();
        }

        private int? GetCurrentUserId()
        {
            var userIdValue =
                User.FindFirstValue(ClaimTypes.NameIdentifier)
                ?? User.FindFirstValue("id")
                ?? User.FindFirstValue("userId");

            if (!int.TryParse(userIdValue, out var userId))
            {
                return null;
            }

            return userId;
        }
    }
}