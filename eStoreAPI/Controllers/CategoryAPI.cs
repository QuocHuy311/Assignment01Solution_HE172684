using BusinessObject.DTO;
using BusinessObject.Models;
using DataAccess.DAO;
using DataAccess.IRepository;
using DataAccess.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace eStoreAPI.Controllers
{
    [Route("api/categories")]
    [ApiController]
    public class CategoryAPI : ControllerBase
    {
        private readonly ICategoryRepository categoryRepository = new CategoryRepository();

        [HttpGet]
        public IActionResult GetAllCategory()
        {
            var categories = categoryRepository.GetCategories();
            var categoryDTOs = categories.Select(c => new CategoryDTO
            {
                CategoryId = c.CategoryId,
                CategoryName = c.CategoryName,
            }).ToList();

            return Ok(categoryDTOs);
        }

        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var category = categoryRepository.GetCategoryById(id);
            if (category == null)
            {
                return NotFound();
            }

            var categoryDTO = new CategoryDTO
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
            };

            return Ok(categoryDTO);
        }

        [HttpPost]
        public IActionResult AddCategory(Category category)
        {
            categoryRepository.InsertCategory(category);
            return Ok();
        }

        [HttpPut]
        public IActionResult UpdateCategory(Category category)
        {
            categoryRepository.UpdateCategory(category);
            return Ok();
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteCategory(int id)
        {
            categoryRepository.DeleteCategory(id);
            return Ok();
        }
    }
}
