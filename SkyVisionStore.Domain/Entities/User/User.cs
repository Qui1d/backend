using SkyVisionStore.Domain.Entities.Cart;
using SkyVisionStore.Domain.Entities.Order;
using SkyVisionStore.Domain.Entities.Refs;
using SkyVisionStore.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkyVisionStore.Domain.Entities.User
{
    public class User
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Username { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        public string Email { get; set; } = string.Empty;

        [Required]
        [StringLength(100, MinimumLength = 6)]
        public string Password { get; set; } = string.Empty;

        public UserRole Role { get; set; } = UserRole.User;

        public DateTime CreatedAt { get; set; }

        public List<CartItem> CartItems { get; set; } = new();

        public List<UserFavorite> Favorites { get; set; } = new();

        public List<Order> Orders { get; set; } = new();

        public List<UserCoupon> UserCoupons { get; set; } = new();
    }
}
