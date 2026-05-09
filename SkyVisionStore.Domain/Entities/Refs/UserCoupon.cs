using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using UserEntity = SkyVisionStore.Domain.Entities.User.User;
using CouponEntity = SkyVisionStore.Domain.Entities.Coupon.Coupon;

namespace SkyVisionStore.Domain.Entities.Refs
{
    public class UserCoupon
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        [Required]
        public int UserId { get; set; }

        [Required]
        public int CouponId { get; set; }

        public bool IsUsed { get; set; }

        public DateTime AssignedAt { get; set; }

        public DateTime? UsedAt { get; set; }

        public UserEntity User { get; set; } = null!;

        public CouponEntity Coupon { get; set; } = null!;
    }
}