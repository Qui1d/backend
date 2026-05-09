using SkyVisionStore.Domain.Entities.Coupon;
using SkyVisionStore.Domain.Entities.User;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

        public User User { get; set; }

        public Coupon Coupon { get; set; }
    }
}
