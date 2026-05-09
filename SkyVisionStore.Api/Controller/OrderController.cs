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
    }
}