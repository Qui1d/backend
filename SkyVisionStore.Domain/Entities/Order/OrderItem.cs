using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using ProductEntity = SkyVisionStore.Domain.Entities.Product.Product;

namespace SkyVisionStore.Domain.Entities.Order
{
    public class OrderItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        [StringLength(150)]
        public string ProductName { get; set; } = string.Empty;

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }

        public Order Order { get; set; } = null!;

        public ProductEntity Product { get; set; } = null!;
    }
}