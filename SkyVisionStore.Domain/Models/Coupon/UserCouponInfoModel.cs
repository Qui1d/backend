namespace SkyVisionStore.Domain.Models.Coupon
{
    public class UserCouponInfoModel
    {
        public int Id { get; set; }

        public int UserId { get; set; }

        public int CouponId { get; set; }

        public string CouponCode { get; set; } = string.Empty;

        public int DiscountPercent { get; set; }

        public bool IsUsed { get; set; }

        public DateTime AssignedAt { get; set; }

        public DateTime? UsedAt { get; set; }
    }
}