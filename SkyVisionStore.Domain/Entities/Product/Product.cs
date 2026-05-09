using SkyVisionStore.Domain.Entities.Cart;
using SkyVisionStore.Domain.Entities.Order;
using SkyVisionStore.Domain.Entities.Refs;
using SkyVisionStore.Domain.Entities.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkyVisionStore.Domain.Entities.Product
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(150, MinimumLength = 2)]
        public string Title { get; set; } = string.Empty;

        [Required]
        [StringLength(150)]
        public string Slug { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Platform { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Genre { get; set; } = string.Empty;

        [Required]
        public decimal Price { get; set; }

        public decimal? OldPrice { get; set; }

        public int? Discount { get; set; }

        [Required]
        [StringLength(500)]
        public string Image { get; set; } = string.Empty;

        [StringLength(500)]
        public string? RecommendedImage { get; set; }

        [Required]
        [StringLength(50)]
        public string Region { get; set; } = string.Empty;

        [Required]
        [StringLength(2000)]
        public string Description { get; set; } = string.Empty;

        [StringLength(2000)]
        public string Requirements { get; set; } = string.Empty;

        public bool IsNew { get; set; }

        public bool IsPopular { get; set; }

        public bool IsUpcoming { get; set; }

        public DateTime CreatedAt { get; set; }

        public List<CartItem> CartItems { get; set; } = new();

        public List<UserFavorite> Favorites { get; set; } = new();

        public List<OrderItem> OrderItems { get; set; } = new();
    }
}
