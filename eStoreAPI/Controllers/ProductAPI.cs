using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace eStoreAPI.Controllers
{
    [Route("api/products")]
    [ApiController]
    public class ProductAPI : ControllerBase
    {
        private readonly IProductRepository productRepository = new ProductRepository();

        [HttpGet]
        public IActionResult GetAllProduct()
        {
            var products = productRepository.GetProducts();

            var productDTOs = products.Select(p => new ProductDTO
            {
                ProductId = p.ProductId,
                ProductName = p.ProductName,
                Weight = p.Weight,
                UnitPrice = p.UnitPrice,
                UnitsInStock = p.UnitsInStock,
                CategoryName = p.Category != null ? p.Category.CategoryName : null
            }).ToList();

            return Ok(productDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult GetProductById(int id)
        {
            var product = productRepository.GetProductById(id);
            return product == null ? NotFound() : Ok(product);
        }

        [HttpPost]
        public IActionResult AddProduct(Product product)
        {
            productRepository.InsertProduct(product);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateProduct(Product product)
        {
            productRepository.UpdateProduct(product);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProduct(int id)
        {
            productRepository.DeleteProduct(id);
            return Ok();
        }
    }
}
