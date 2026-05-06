using SkyVisionStore.Domain.Entities.Order;

namespace SkyVisionStore.Domain.Models.Order
{
    public class CreateOrderModel
    {
        public int UserId { get; set; }
        public decimal TotalAmount { get; set; }
        public List<OrderItem> Items { get; set; } = new();
    }
}