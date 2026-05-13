using SkyVisionStore.Domain.Models.Coupon;

namespace SkyVisionStore.BusinessLogic.Interface
{
    public interface ICouponActions
    {
        List<CouponInfoModel> GetAll();

        CouponInfoModel? GetById(int id);

        CouponInfoModel? GetByCode(string code);

        CouponInfoModel Create(CouponCreateModel coupon);

        CouponInfoModel? Update(int id, CouponUpdateModel updatedCoupon);

        bool Delete(int id);

        List<UserCouponInfoModel> GetUserCoupons(int userId);

        UserCouponInfoModel? ActivateCoupon(ActivateCouponModel model);
    }
}