using BusinessObject.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

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
                var products = JsonConvert.DeserializeObject<List<Product>>(json);
                return View(products); 
            }
            else
            {
                ViewBag.Error = "Không lấy được dữ liệu từ API";
                return View(new List<Product>());
            }
        }
    }
}
