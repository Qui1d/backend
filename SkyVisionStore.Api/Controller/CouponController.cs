using Microsoft.AspNetCore.Mvc;
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

        [HttpGet("all")]
        public IActionResult GetAllCoupons()
        {
            return Ok(_couponActions.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetCouponById(int id)
        {
            var coupon = _couponActions.GetById(id);

            if (coupon == null)
            {
                return NotFound(new { Message = $"Coupon with ID {id} not found" });
            }

            return Ok(coupon);
        }

        [HttpGet("code/{code}")]
        public IActionResult GetCouponByCode(string code)
        {
            var coupon = _couponActions.GetByCode(code);

            if (coupon == null)
            {
                return NotFound(new { Message = $"Coupon with code {code} not found" });
            }

            return Ok(coupon);
        }

        [HttpPost]
        public IActionResult CreateCoupon([FromBody] CouponCreateModel coupon)
        {
            var createdCoupon = _couponActions.Create(coupon);

            return Created($"/api/coupon/{createdCoupon.Id}", createdCoupon);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateCoupon(int id, [FromBody] CouponUpdateModel updatedCoupon)
        {
            var coupon = _couponActions.Update(id, updatedCoupon);

            if (coupon == null)
            {
                return NotFound(new { Message = $"Coupon with ID {id} not found" });
            }

            return Ok(coupon);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCoupon(int id)
        {
            var deleted = _couponActions.Delete(id);

            if (!deleted)
            {
                return NotFound(new { Message = $"Coupon with ID {id} not found" });
            }

            return NoContent();
        }

        [HttpGet("user/{userId}")]
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