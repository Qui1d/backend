using SkyVisionStore.Domain.Enums;
using System.ComponentModel.DataAnnotations;

namespace SkyVisionStore.Domain.Models.Coupon
{
    public class CouponUpdateModel
    {
        [Required]
        [StringLength(30, MinimumLength = 3)]
        public string Code { get; set; } = string.Empty;

        [StringLength(300)]
        public string Description { get; set; } = string.Empty;

        [Required]
        [Range(1, 100)]
        public int DiscountPercent { get; set; }

        public CouponStatus Status { get; set; } = CouponStatus.Active;
    }
}