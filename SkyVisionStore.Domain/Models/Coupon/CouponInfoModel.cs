using SkyVisionStore.Domain.Enums;

namespace SkyVisionStore.Domain.Models.Coupon
{
    public class CouponInfoModel
    {
        public int Id { get; set; }

        public string Code { get; set; } = string.Empty;

        public string Description { get; set; } = string.Empty;

        public int DiscountPercent { get; set; }

        public CouponStatus Status { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}