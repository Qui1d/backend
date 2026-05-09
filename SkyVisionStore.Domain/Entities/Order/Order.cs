using SkyVisionStore.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserEntity = SkyVisionStore.Domain.Entities.User.User;

namespace SkyVisionStore.Domain.Entities.Order
{
    public class Order
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        public DateTime CreatedAt { get; set; }

        public OrderStatus Status { get; set; } = OrderStatus.Pending;

        [Required]
        public decimal TotalAmount { get; set; }

        public UserEntity User { get; set; } = null!;

        public List<OrderItem> Items { get; set; } = new();
    }
}