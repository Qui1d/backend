using Microsoft.AspNetCore.Mvc;
using SkyVisionStore.BusinessLogic;
using SkyVisionStore.BusinessLogic.Interfaces;
using SkyVisionStore.Domain.Entities.Product;

namespace SkyVisionStore.Api.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductBL _productBL;

        public ProductController()
        {
            var bl = new SkyVisionStore.BusinessLogic.BusinessLogic();
            _productBL = bl.GetProductBL();
        }

        [HttpGet("all")]
        public IActionResult GetAllProducts()
        {
            return Ok(_productBL.GetAll());
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = _productBL.GetById(id);

            if (product == null)
            {
                return NotFound(new { Message = $"Product with ID {id} not found" });
            }

            return Ok(product);
        }

        [HttpPost]
        public IActionResult CreateProduct([FromBody] Product product)
        {
            var createdProduct = _productBL.Create(product);
            return Created($"/api/product/{createdProduct.Id}", createdProduct);
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProduct(int id, [FromBody] Product updatedProduct)
        {
            var product = _productBL.Update(id, updatedProduct);

            if (product == null)
            {
                return NotFound(new { Message = $"Product with ID {id} not found" });
            }

            return Ok(product);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            var deleted = _productBL.Delete(id);

            if (!deleted)
            {
                return NotFound(new { Message = $"Product with ID {id} not found" });
            }

            return NoContent();
        }
    }
}