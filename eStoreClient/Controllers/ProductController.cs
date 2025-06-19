using BusinessObject.Models;
using BusinessObject.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace eStoreClient.Controllers
{
    public class ProductController : Controller
    {
        private readonly HttpClient _client;

        public ProductController(IConfiguration configuration)
        {
            var baseUrl = configuration["ApiSettings:BaseUrl"]; 
            _client = new HttpClient { BaseAddress = new Uri(baseUrl) };
        }

        public async Task<IActionResult> Index()
        {
            HttpResponseMessage response = await _client.GetAsync("products"); 
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<ProductDTO>>(json);
                return View(products); 
            }
            else
            {
                ViewBag.Error = "Không lấy được dữ liệu từ API";
                return View(new List<ProductDTO>());
            }
        }

        public async Task<IActionResult> Create()
        {
            ViewBag.CategoryList = await GetCategorySelectList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ProductDTO product)
        {
            var json = JsonConvert.SerializeObject(product);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("products", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }
            else
            {
                ViewBag.Error = "Không thêm được sản phẩm";
                ViewBag.CategoryList = await GetCategorySelectList();
                return View(product);
            }
        }

        // GET: Product/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            HttpResponseMessage response = await _client.GetAsync($"products/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var json = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<ProductDTO>(json);

            ViewBag.CategoryList = await SelectListByCategoryId(product.CategoryId.GetValueOrDefault()); 
            
            return View(product);
        }

        // POST: Product/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(ProductDTO product)
        {
            if (ModelState.IsValid)
            {
                var json = JsonConvert.SerializeObject(product);
                var content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await _client.PutAsync("products", content);
                if (response.IsSuccessStatusCode)
                {
                    return RedirectToAction(nameof(Index));
                }
                else
                {
                    ViewBag.Error = "Cập nhật thất bại!";
                    ViewBag.CategoryList = await SelectListByCategoryId(product.CategoryId.GetValueOrDefault());
                }
            }

            ViewBag.CategoryList = await SelectListByCategoryId(product.CategoryId.GetValueOrDefault());
            return View(product);
        }

        public async Task<IActionResult> Details(int id)
        {
            HttpResponseMessage response = await _client.GetAsync($"products/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var json = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<ProductDTO>(json);
            return View(product);
        }

        public async Task<IActionResult> Delete(int id)
        {
            HttpResponseMessage response = await _client.GetAsync($"products/{id}");
            if (!response.IsSuccessStatusCode)
            {
                return NotFound();
            }

            var json = await response.Content.ReadAsStringAsync();
            var product = JsonConvert.DeserializeObject<ProductDTO>(json);
            return View(product);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            HttpResponseMessage response = await _client.DeleteAsync($"products/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Error = "Xóa thất bại!";
            return RedirectToAction(nameof(Index));
        }

        private async Task<SelectList> GetCategorySelectList()
        {
            var categoryResponse = await _client.GetAsync("categories");
            if (categoryResponse.IsSuccessStatusCode)
            {
                var json = await categoryResponse.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<List<CategoryDTO>>(json);

                return new SelectList(categories, "CategoryId", "CategoryName");
            }

            return new SelectList(new List<CategoryDTO>(), "CategoryId", "CategoryName");
        }
        
        private async Task<SelectList> SelectListByCategoryId(int categoryId)
        {
            var categoryResponse = await _client.GetAsync("categories");
            if (categoryResponse.IsSuccessStatusCode)
            {
                var json = await categoryResponse.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<List<CategoryDTO>>(json);

                return new SelectList(categories, "CategoryId", "CategoryName", categoryId);
            }

            return new SelectList(new List<CategoryDTO>(), "CategoryId", "CategoryName", categoryId);
        }

    }
}
