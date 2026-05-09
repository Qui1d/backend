using SkyVisionStore.BusinessLogic.Interface;
using SkyVisionStore.Domain.Enums;
using SkyVisionStore.Domain.Models.Coupon;
using CouponEntity = SkyVisionStore.Domain.Entities.Coupon.Coupon;
using UserCouponEntity = SkyVisionStore.Domain.Entities.Refs.UserCoupon;

namespace SkyVisionStore.BusinessLogic.Core.Coupon
{
    public class CouponActions : ICouponActions
    {
        private static readonly List<CouponEntity> _coupons = new();
        private static readonly List<UserCouponEntity> _userCoupons = new();

        private static int _nextCouponId = 1;
        private static int _nextUserCouponId = 1;

        public List<CouponInfoModel> GetAll()
        {
            return _coupons
                .Select(ToInfoModel)
                .ToList();
        }

        public CouponInfoModel? GetById(int id)
        {
            var coupon = _coupons.FirstOrDefault(c => c.Id == id);

            if (coupon == null)
            {
                return null;
            }

            return ToInfoModel(coupon);
        }

        public CouponInfoModel? GetByCode(string code)
        {
            var coupon = _coupons.FirstOrDefault(c =>
                c.Code.Equals(code, StringComparison.OrdinalIgnoreCase));

            if (coupon == null)
            {
                return null;
            }

            return ToInfoModel(coupon);
        }

        public CouponInfoModel Create(CouponCreateModel coupon)
        {
            var newCoupon = new CouponEntity
            {
                Id = _nextCouponId++,
                Code = coupon.Code,
                Description = coupon.Description,
                DiscountPercent = coupon.DiscountPercent,
                Status = coupon.Status,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = null
            };

            _coupons.Add(newCoupon);

            return ToInfoModel(newCoupon);
        }

        public CouponInfoModel? Update(int id, CouponUpdateModel updatedCoupon)
        {
            var existingCoupon = _coupons.FirstOrDefault(c => c.Id == id);

            if (existingCoupon == null)
            {
                return null;
            }

            existingCoupon.Code = updatedCoupon.Code;
            existingCoupon.Description = updatedCoupon.Description;
            existingCoupon.DiscountPercent = updatedCoupon.DiscountPercent;
            existingCoupon.Status = updatedCoupon.Status;

            return ToInfoModel(existingCoupon);
        }

        public bool Delete(int id)
        {
            var coupon = _coupons.FirstOrDefault(c => c.Id == id);

            if (coupon == null)
            {
                return false;
            }

            _coupons.Remove(coupon);

            return true;
        }

        public List<UserCouponEntity> GetUserCoupons(int userId)
        {
            return _userCoupons
                .Where(c => c.UserId == userId)
                .ToList();
        }

        public UserCouponEntity? ActivateCoupon(ActivateCouponModel model)
        {
            var coupon = _coupons.FirstOrDefault(c =>
                c.Code.Equals(model.CouponCode, StringComparison.OrdinalIgnoreCase) &&
                c.Status == CouponStatus.Active);

            if (coupon == null)
            {
                return null;
            }

            var existingUserCoupon = _userCoupons.FirstOrDefault(c =>
                c.UserId == model.UserId &&
                c.CouponId == coupon.Id);

            if (existingUserCoupon != null)
            {
                return null;
            }

            var userCoupon = new UserCouponEntity
            {
                Id = _nextUserCouponId++,
                UserId = model.UserId,
                CouponId = coupon.Id,
                IsUsed = false,
                AssignedAt = DateTime.UtcNow,
                UsedAt = null
            };

            _userCoupons.Add(userCoupon);

            return userCoupon;
        }

        private static CouponInfoModel ToInfoModel(CouponEntity coupon)
        {
            return new CouponInfoModel
            {
                Id = coupon.Id,
                Code = coupon.Code,
                Description = coupon.Description,
                DiscountPercent = coupon.DiscountPercent,
                Status = coupon.Status,
                CreatedAt = coupon.CreatedAt
            };
        }
    }
}