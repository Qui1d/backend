using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SkyVisionStore.BusinessLogic.Interface;
using SkyVisionStore.Domain.Models.Product;

namespace SkyVisionStore.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductActions _productActions;

        public ProductController()
        {
            var bl = new SkyVisionStore.BusinessLogic.BusinessLogic();
            _productActions = bl.GetProductActions();
        }

        [AllowAnonymous]
        [HttpGet("all")]
        public IActionResult GetAllProducts()
        {
            return Ok(_productActions.GetAll());
        }

        [AllowAnonymous]
        [HttpGet("slug/{slug}")]
        public IActionResult GetProductBySlug(string slug)
        {
            var product = _productActions.GetBySlug(slug);

            if (product == null)
            {
                return NotFound(new { Message = $"Product with slug '{slug}' not found" });
            }

            return Ok(product);
        }

        [AllowAnonymous]
        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productActions.GetById(id);

            if (product == null)
            {
                return NotFound(new { Message = $"Product with ID {id} not found" });
            }

            return Ok(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost("create")]
        public IActionResult CreateProduct([FromBody] ProductCreateModel product)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var createdProduct = _productActions.Create(product);

            return Created($"/api/product/{createdProduct.Id}", createdProduct);
        }

        [Authorize(Roles = "Admin")]
        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductUpdateModel updatedProduct)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var product = _productActions.Update(id, updatedProduct);

            if (product == null)
            {
                return NotFound(new { Message = $"Product with ID {id} not found" });
            }

            return Ok(product);
        }

        [Authorize(Roles = "Admin")]
        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var deleted = _productActions.Delete(id);

            if (!deleted)
            {
                return NotFound(new { Message = $"Product with ID {id} not found" });
            }

            return NoContent();
        }
    }
}