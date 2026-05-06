namespace SkyVisionStore.Domain.Models.Coupon
{
    public class ActivateCouponModel
    {
        public int UserId { get; set; }
        public string CouponCode { get; set; } = string.Empty;
    }
}