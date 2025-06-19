using BusinessObject.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace eStoreClient.Controllers
{
    public class CategoryController : Controller
    {
        private readonly HttpClient _client;

        public CategoryController(IConfiguration configuration)
        {
            var baseUrl = configuration["ApiSettings:BaseUrl"];
            _client = new HttpClient { BaseAddress = new Uri(baseUrl) };
        }

        // GET: Category
        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync("categories");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var categories = JsonConvert.DeserializeObject<List<CategoryDTO>>(json);
                return View(categories);
            }
            ViewBag.Error = "Không lấy được danh sách danh mục!";
            return View(new List<CategoryDTO>());
        }

        // GET: Category/Create
        public IActionResult Create() => View();

        // POST: Category/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryDTO category)
        {
            var json = JsonConvert.SerializeObject(category);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("categories", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Error = "Không thêm được danh mục!";
            return View(category);
        }

        // GET: Category/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            var response = await _client.GetAsync($"categories/{id}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var category = JsonConvert.DeserializeObject<CategoryDTO>(json);
            return View(category);
        }

        // POST: Category/Edit
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(CategoryDTO category)
        {
            var json = JsonConvert.SerializeObject(category);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("categories", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Error = "Cập nhật thất bại!";
            return View(category);
        }

        // GET: Category/Details/5
        public async Task<IActionResult> Details(int id)
        {
            var response = await _client.GetAsync($"categories/{id}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var category = JsonConvert.DeserializeObject<CategoryDTO>(json);
            return View(category);
        }

        // GET: Category/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.GetAsync($"categories/{id}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var category = JsonConvert.DeserializeObject<CategoryDTO>(json);
            return View(category);
        }

        // POST: Category/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _client.DeleteAsync($"categories/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Error = "Xóa thất bại!";
            return RedirectToAction(nameof(Index));
        }
    }
}
