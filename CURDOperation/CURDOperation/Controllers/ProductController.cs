using CURDOperation.Models;
using CURDOperation.Service.IService;
using Microsoft.AspNetCore.Mvc;

namespace CURDOperation.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;

        public ProductController(IProductService productService)
        {
            _productService = productService;
        }

        [HttpPost]
        public async Task<IActionResult> CreateProduct([FromBody] Product product)
        {
            if (product == null) return BadRequest("Product cannot be null");

            await _productService.CreateProductAsync(product);
            return CreatedAtAction(nameof(GetProductById), new { id = product.ProductId }, product);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var product = await _productService.GetProductByIdAsync(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPut("update/{productId}")]
        public async Task<IActionResult> UpdateProduct(int productId, [FromBody] Product product)
        {
            if (productId != product.ProductId) return BadRequest("Product ID mismatch");

            var result = await _productService.UpdateProductAsync(productId, product);
            if (!result) return NotFound();
            return Ok("Product updated successfully");
        }

        [HttpDelete("delete/{productId}")]
        public async Task<IActionResult> DeleteProduct(int productId)
        {
            var result = await _productService.DeleteProductAsync(productId);
            if (!result) return NotFound();
            return Ok("Product deleted successfully");
        }

        [HttpGet("pagination")]
        public async Task<IActionResult> GetProducts([FromQuery] int pageNumber = 1, [FromQuery] int pageSize = 10)
        {
            var products = await _productService.GetProductsAsync(pageNumber, pageSize);
            if (!products.Any()) return NotFound("No products found");
            return Ok(products);
        }
    }
}
