using SkyVisionStore.Domain.Entities.Refs;
using SkyVisionStore.Domain.Entities.User;
using SkyVisionStore.Domain.Enums;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace SkyVisionStore.Domain.Entities.Coupon
{
    public class Coupon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string Code { get; set; } = string.Empty;

        [Required]
        public int DiscountPercent { get; set; }

        public CouponStatus Status { get; set; } = CouponStatus.Active;

        public DateTime CreatedAt { get; set; }

        public DateTime? ExpiresAt { get; set; }

        public List<UserCoupon> UserCoupons { get; set; } = new();
    }
}
