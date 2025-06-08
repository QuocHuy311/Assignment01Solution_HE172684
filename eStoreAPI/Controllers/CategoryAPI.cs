using BusinessObject.Models;
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
        public IActionResult GetAllCategory() => Ok(categoryRepository.GetCategories());

        [HttpGet("{id}")]
        public IActionResult GetCategoryById(int id)
        {
            var Category = categoryRepository.GetCategoryById(id);
            return Category == null ? NotFound() : Ok(Category);
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
