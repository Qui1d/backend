using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserEntity = SkyVisionStore.Domain.Entities.User.User;
using ProductEntity = SkyVisionStore.Domain.Entities.Product.Product;

namespace SkyVisionStore.Domain.Entities.Cart
{
    public class CartItem
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Quantity { get; set; } = 1;

        public DateTime AddedAt { get; set; }

        public UserEntity User { get; set; } = null!;

        public ProductEntity Product { get; set; } = null!;
    }
}