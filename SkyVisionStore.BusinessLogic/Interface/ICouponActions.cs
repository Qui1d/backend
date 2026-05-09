using SkyVisionStore.Domain.Models.Coupon;
using UserCouponEntity = SkyVisionStore.Domain.Entities.Refs.UserCoupon;

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

        List<UserCouponEntity> GetUserCoupons(int userId);

        UserCouponEntity? ActivateCoupon(ActivateCouponModel model);
    }
}