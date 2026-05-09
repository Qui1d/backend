using SkyVisionStore.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace SkyVisionStore.Domain.Models.Order
{
    public class OrderUpdateModel
    {
        [Required]
        public int UserId { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        [Required]
        public decimal TotalAmount { get; set; }

        public List<OrderItemCreateModel> Items { get; set; } = new();
    }
}