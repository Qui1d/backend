using SkyVisionStore.Domain.Entities.User;
using SkyVisionStore.Domain.Models.Coupon;

namespace SkyVisionStore.BusinessLogic.Interface
{
    public interface ICouponActions
    {
        List<UserCoupon> GetUserCoupons(int userId);
        UserCoupon? ActivateCoupon(ActivateCouponModel model);
    }
}