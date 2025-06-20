using BusinessObject.DTO;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System.Text;

namespace eStoreClient.Controllers
{
    public class OrderController : Controller
    {
        private readonly HttpClient _client;

        public OrderController(IConfiguration configuration)
        {
            var baseUrl = configuration["ApiSettings:BaseUrl"];
            _client = new HttpClient { BaseAddress = new Uri(baseUrl) };
        }

        public async Task<IActionResult> Index()
        {
            var role = HttpContext.Session.GetString("Role");
            int? memberId = HttpContext.Session.GetInt32("MemberId");

            var request = new HttpRequestMessage(HttpMethod.Get, "orders");
            request.Headers.Add("Role", role ?? "");

            if (memberId.HasValue)
            {
                request.Headers.Add("MemberId", memberId.Value.ToString());
            }

            HttpResponseMessage response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Không thể tải danh sách đơn hàng.";
                return View(new List<OrderDTO>());
            }

            var json = await response.Content.ReadAsStringAsync();
            var orders = JsonConvert.DeserializeObject<List<OrderDTO>>(json);

            return View(orders);
        }


        public async Task<IActionResult> Create()
        {
            ViewBag.Members = await GetMemberList();
            ViewBag.Products = await GetProductList();
            ViewBag.ProductData = await GetProductDataList();
            return View(new OrderDTO { OrderDate = DateTime.Now });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(OrderDTO order)
        {
            if (!ModelState.IsValid || order.OrderDetails == null || !order.OrderDetails.Any())
            {
                ViewBag.Error = "Please add at least one product.";
                ViewBag.Members = await GetMemberList();
                ViewBag.Products = await GetProductList();
                return View(order);
            }

            var json = JsonConvert.SerializeObject(order);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("orders", content);

            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Error = "Order creation failed.";
            ViewBag.Members = await GetMemberList();
            ViewBag.Products = await GetProductList();
            return View(order);
        }

        public async Task<IActionResult> Details(int id)
        {
            var role = HttpContext.Session.GetString("Role");
            var memberId = HttpContext.Session.GetInt32("MemberId");

            var request = new HttpRequestMessage(HttpMethod.Get, $"orders/{id}");
            request.Headers.Add("Role", role ?? "");
            request.Headers.Add("MemberId", memberId?.ToString() ?? "");

            var response = await _client.SendAsync(request);

            if (!response.IsSuccessStatusCode)
            {
                ViewBag.Error = "Không thể tải chi tiết đơn hàng.";
                return RedirectToAction(nameof(Index));
            }

            var json = await response.Content.ReadAsStringAsync();
            var order = JsonConvert.DeserializeObject<OrderViewModel>(json);
            return View(order);
        }


        private async Task<List<SelectListItem>> GetMemberList()
        {
            var res = await _client.GetAsync("members");
            if (!res.IsSuccessStatusCode) return new();
            var json = await res.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<MemberDTO>>(json);
            return list.Select(m => new SelectListItem { Value = m.MemberId.ToString(), Text = m.Email }).ToList();
        }

        private async Task<List<SelectListItem>> GetProductList()
        {
            var res = await _client.GetAsync("products");
            if (!res.IsSuccessStatusCode) return new();
            var json = await res.Content.ReadAsStringAsync();
            var list = JsonConvert.DeserializeObject<List<ProductDTO>>(json);
            return list.Select(p => new SelectListItem { Value = p.ProductId.ToString(), Text = p.ProductName }).ToList();
        }

        private async Task<List<ProductDTO>> GetProductDataList()
        {
            var response = await _client.GetAsync("products"); 
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var products = JsonConvert.DeserializeObject<List<ProductDTO>>(json);
                return products ?? new List<ProductDTO>();
            }
            return new List<ProductDTO>();
        }

    }
}
