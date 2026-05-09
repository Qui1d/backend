using System.ComponentModel.DataAnnotations;

namespace SkyVisionStore.Domain.Models.Order
{
    public class OrderItemCreateModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        [StringLength(150)]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        [Range(1, 100)]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }
    }
}