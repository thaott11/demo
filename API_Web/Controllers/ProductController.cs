using API_Web.Models;
using API_Web.Repository;
using API_Web.Service;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API_Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IProductService _productService;
        private readonly IUploatFileRepo _uploatFileRepo;

        public ProductController(IProductService productService, IUploatFileRepo uploatFileRepo)
        {
            _productService = productService;
            _uploatFileRepo = uploatFileRepo;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var products = await _productService.GetAll();
            var baseUrl = $"{Request.Scheme}://{Request.Host}";
            foreach (var product in products)
            {
                if (!string.IsNullOrEmpty(product.ImagePath))
                {
                    product.ImagePath = baseUrl + product.ImagePath;
                }
            }
            return Ok(products);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            var product = await _productService.GetById(id);
            if (product == null) return NotFound();
            return Ok(product);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] Products products, IFormFile? file)
        {
            var product =  await _productService.Create(products);
            if (product != null)
            {
                if (file != null)
                {
                    products.ImagePath = await _uploatFileRepo.UpLoatFile(file);
                }
            }
            return Ok(products);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Update(int id, [FromForm] Products products, IFormFile? file)
        {
            var existingProduct = await _productService.GetById(id);
            if (existingProduct == null)
            {
                return NotFound();
            }

            if (file != null)
            {
                try
                {
                    var imagePath = await _uploatFileRepo.UpLoatFile(file);
                    existingProduct.ImagePath = imagePath;
                }
                catch (Exception ex)
                {
                    return BadRequest($"File upload failed: {ex.Message}");
                }
            }

            existingProduct.ProductName = products.ProductName;
            existingProduct.Price = products.Price;
            existingProduct.StockQuantity = products.StockQuantity;

            await _productService.Update(existingProduct);
            return Ok(existingProduct);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var product = await _productService.GetById(id);
            if (product == null)
            {
                return NotFound();
            }

            var isDeleted = await _productService.Delete(id);
            if (isDeleted)
            {
                return NoContent();
            }

            return BadRequest("Product deletion failed.");
        }
    }
}
