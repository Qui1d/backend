using Microsoft.AspNetCore.Mvc;
using SkyVisionStore.BusinessLogic;
using SkyVisionStore.BusinessLogic.Interface;
using SkyVisionStore.Domain.Models.Coupon;

namespace SkyVisionStore.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class CouponController : ControllerBase
    {
        private readonly ICouponActions _couponActions;

        public CouponController()
        {
            var bl = new SkyVisionStore.BusinessLogic.BusinessLogic();
            _couponActions = bl.GetCouponActions();
        }

        [HttpGet("{userId}")]
        public IActionResult GetUserCoupons(int userId)
        {
            var coupons = _couponActions.GetUserCoupons(userId);
            return Ok(coupons);
        }

        [HttpPost("activate")]
        public IActionResult ActivateCoupon([FromBody] ActivateCouponModel model)
        {
            if (model == null)
            {
                return BadRequest(new { Message = "Coupon activation data is required" });
            }

            var activatedCoupon = _couponActions.ActivateCoupon(model);

            if (activatedCoupon == null)
            {
                return BadRequest(new { Message = "Coupon activation failed" });
            }

            return Ok(activatedCoupon);
        }
    }
}