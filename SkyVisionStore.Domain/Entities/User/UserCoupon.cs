using SkyVisionStore.Domain.Enums;

namespace SkyVisionStore.Domain.Entities.User
{
    public class UserCoupon
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string CouponCode { get; set; } = string.Empty;
        public CouponStatus Status { get; set; } = CouponStatus.Active;
        public DateTime ActivatedAt { get; set; }
    }
}