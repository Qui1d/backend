using SkyVisionStore.Domain.Enums;

namespace SkyVisionStore.Domain.Models.Order
{
    public class OrderInfoModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public OrderStatus Status { get; set; }

        public decimal TotalAmount { get; set; }

        public List<OrderItemInfoModel> Items { get; set; } = new();
    }
}