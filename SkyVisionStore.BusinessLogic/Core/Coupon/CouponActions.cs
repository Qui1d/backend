using Microsoft.EntityFrameworkCore;
using SkyVisionStore.BusinessLogic.Interface;
using SkyVisionStore.DataAccess.Context;
using SkyVisionStore.Domain.Enums;
using SkyVisionStore.Domain.Models.Coupon;
using CouponEntity = SkyVisionStore.Domain.Entities.Coupon.Coupon;
using UserCouponEntity = SkyVisionStore.Domain.Entities.Refs.UserCoupon;

namespace SkyVisionStore.BusinessLogic.Core.Coupon
{
    public class CouponActions : ICouponActions
    {
        public List<CouponInfoModel> GetAll()
        {
            using var db = new SkyVisionStoreContext();

            return db.Coupons
                .OrderBy(c => c.Id)
                .Select(c => ToInfoModel(c))
                .ToList();
        }

        public CouponInfoModel? GetById(int id)
        {
            using var db = new SkyVisionStoreContext();

            var coupon = db.Coupons
                .FirstOrDefault(c => c.Id == id);

            if (coupon == null)
            {
                return null;
            }

            return ToInfoModel(coupon);
        }

        public CouponInfoModel? GetByCode(string code)
        {
            using var db = new SkyVisionStoreContext();

            var coupon = db.Coupons
                .FirstOrDefault(c => c.Code.ToLower() == code.ToLower());

            if (coupon == null)
            {
                return null;
            }

            return ToInfoModel(coupon);
        }

        public CouponInfoModel Create(CouponCreateModel coupon)
        {
            using var db = new SkyVisionStoreContext();

            var existingCoupon = db.Coupons
                .FirstOrDefault(c => c.Code.ToLower() == coupon.Code.ToLower());

            if (existingCoupon != null)
            {
                return ToInfoModel(existingCoupon);
            }

            var newCoupon = new CouponEntity
            {
                Code = coupon.Code,
                Description = coupon.Description,
                DiscountPercent = coupon.DiscountPercent,
                Status = coupon.Status,
                CreatedAt = DateTime.UtcNow,
                ExpiresAt = null
            };

            db.Coupons.Add(newCoupon);
            db.SaveChanges();

            return ToInfoModel(newCoupon);
        }

        public CouponInfoModel? Update(int id, CouponUpdateModel updatedCoupon)
        {
            using var db = new SkyVisionStoreContext();

            var existingCoupon = db.Coupons
                .FirstOrDefault(c => c.Id == id);

            if (existingCoupon == null)
            {
                return null;
            }

            var duplicateCoupon = db.Coupons.FirstOrDefault(c =>
                c.Id != id &&
                c.Code.ToLower() == updatedCoupon.Code.ToLower());

            if (duplicateCoupon != null)
            {
                return null;
            }

            existingCoupon.Code = updatedCoupon.Code;
            existingCoupon.Description = updatedCoupon.Description;
            existingCoupon.DiscountPercent = updatedCoupon.DiscountPercent;
            existingCoupon.Status = updatedCoupon.Status;

            db.SaveChanges();

            return ToInfoModel(existingCoupon);
        }

        public bool Delete(int id)
        {
            using var db = new SkyVisionStoreContext();

            var coupon = db.Coupons
                .FirstOrDefault(c => c.Id == id);

            if (coupon == null)
            {
                return false;
            }

            var userCoupons = db.UserCoupons
                .Where(uc => uc.CouponId == id)
                .ToList();

            db.UserCoupons.RemoveRange(userCoupons);
            db.Coupons.Remove(coupon);
            db.SaveChanges();

            return true;
        }

        public List<UserCouponInfoModel> GetUserCoupons(int userId)
        {
            using var db = new SkyVisionStoreContext();

            return db.UserCoupons
                .Include(uc => uc.Coupon)
                .Where(uc => uc.UserId == userId)
                .OrderBy(uc => uc.Id)
                .Select(uc => ToUserCouponInfoModel(uc))
                .ToList();
        }

        public UserCouponInfoModel? ActivateCoupon(ActivateCouponModel model)
        {
            using var db = new SkyVisionStoreContext();

            var userExists = db.Users.Any(u => u.Id == model.UserId);

            if (!userExists)
            {
                return null;
            }

            var coupon = db.Coupons.FirstOrDefault(c =>
                c.Code.ToLower() == model.CouponCode.ToLower() &&
                c.Status == CouponStatus.Active);

            if (coupon == null)
            {
                return null;
            }

            var alreadyActivated = db.UserCoupons.Any(c =>
                c.UserId == model.UserId &&
                c.CouponId == coupon.Id);

            if (alreadyActivated)
            {
                return null;
            }

            var userCoupon = new UserCouponEntity
            {
                UserId = model.UserId,
                CouponId = coupon.Id,
                IsUsed = false,
                AssignedAt = DateTime.UtcNow,
                UsedAt = null
            };

            db.UserCoupons.Add(userCoupon);
            db.SaveChanges();

            userCoupon.Coupon = coupon;

            return ToUserCouponInfoModel(userCoupon);
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

        private static UserCouponInfoModel ToUserCouponInfoModel(UserCouponEntity userCoupon)
        {
            return new UserCouponInfoModel
            {
                Id = userCoupon.Id,
                UserId = userCoupon.UserId,
                CouponId = userCoupon.CouponId,
                CouponCode = userCoupon.Coupon != null ? userCoupon.Coupon.Code : string.Empty,
                DiscountPercent = userCoupon.Coupon != null ? userCoupon.Coupon.DiscountPercent : 0,
                IsUsed = userCoupon.IsUsed,
                AssignedAt = userCoupon.AssignedAt,
                UsedAt = userCoupon.UsedAt
            };
        }
    }
}