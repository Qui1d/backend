using SkyVisionStore.BusinessLogic.Interface;
using SkyVisionStore.Domain.Entities.User;
using SkyVisionStore.Domain.Enums;
using SkyVisionStore.Domain.Models.Coupon;

namespace SkyVisionStore.BusinessLogic.Core.Coupon
{
    public class CouponActions : ICouponActions
    {
        private static readonly List<UserCoupon> _userCoupons = new();
        private static int _nextId = 1;

        public List<UserCoupon> GetUserCoupons(int userId)
        {
            return _userCoupons.Where(c => c.UserId == userId).ToList();
        }

        public UserCoupon? ActivateCoupon(ActivateCouponModel model)
        {
            var existingCoupon = _userCoupons.FirstOrDefault(c =>
                c.UserId == model.UserId &&
                c.CouponCode == model.CouponCode);

            if (existingCoupon != null)
            {
                return null;
            }

            var coupon = new UserCoupon
            {
                Id = _nextId++,
                UserId = model.UserId,
                CouponCode = model.CouponCode,
                Status = CouponStatus.Active,
                ActivatedAt = DateTime.UtcNow
            };

            _userCoupons.Add(coupon);
            return coupon;
        }
    }
}