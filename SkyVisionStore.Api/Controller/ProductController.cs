using Microsoft.AspNetCore.Mvc;
using SkyVisionStore.BusinessLogic.Interface;

using ProductEntity = SkyVisionStore.Domain.Entities.Product.Product;

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

        [HttpGet("all")]
        public IActionResult GetAllProducts()
        {
            return Ok(_productActions.GetAll());
        }

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

        [HttpPost]
        public IActionResult CreateProduct([FromBody] ProductEntity product)
        {
            var createdProduct = _productActions.Create(product);

            return Created($"/api/product/{createdProduct.Id}", createdProduct);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] ProductEntity updatedProduct)
        {
            var product = _productActions.Update(id, updatedProduct);

            if (product == null)
            {
                return NotFound(new { Message = $"Product with ID {id} not found" });
            }

            return Ok(product);
        }

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