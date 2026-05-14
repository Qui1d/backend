using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkyVisionStore.BusinessLogic.Interface;
using SkyVisionStore.Domain.Enums;
using SkyVisionStore.Domain.Models.Order;
using System.Security.Claims;

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

        [Authorize]
        [HttpGet("my")]
        public IActionResult GetMyOrders()
        {
            var userId = GetCurrentUserId();

            if (userId == null)
            {
                return Unauthorized(new { Message = "Invalid token" });
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
                return Unauthorized(new { Message = "Invalid token" });
            }

            var order = _orderActions.CheckoutFromCart(userId.Value);

            if (order == null)
            {
                return BadRequest(new { Message = "Cart is empty or user not found" });
            }

            return Ok(order);
        }

        [HttpGet("all")]
        public IActionResult GetAllOrders()
        {
            return Ok(_orderActions.GetAll());
        }

        [HttpGet("user/{userId:int}")]
        public IActionResult GetOrdersByUserId(int userId)
        {
            var orders = _orderActions.GetOrdersByUserId(userId);

            return Ok(orders);
        }

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

        [HttpPost("create")]
        public IActionResult CreateOrder([FromBody] CreateOrderModel model)
        {
            if (model == null)
            {
                return BadRequest(new { Message = "Order data is required" });
            }

            var order = _orderActions.CreateOrder(model);

            return Created($"/api/order/{order.Id}", order);
        }

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
            var userIdClaim = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

            if (!int.TryParse(userIdClaim, out var userId))
            {
                return null;
            }

            return userId;
        }
    }
}