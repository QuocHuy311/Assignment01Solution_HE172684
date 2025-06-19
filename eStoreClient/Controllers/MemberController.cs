using BusinessObject.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace eStoreClient.Controllers
{
    public class MemberController : Controller
    {
        private readonly HttpClient _client;

        public MemberController(IConfiguration configuration)
        {
            var baseUrl = configuration["ApiSettings:BaseUrl"];
            _client = new HttpClient { BaseAddress = new Uri(baseUrl) };
        }

        public async Task<IActionResult> Index()
        {
            var response = await _client.GetAsync("members");
            if (response.IsSuccessStatusCode)
            {
                var json = await response.Content.ReadAsStringAsync();
                var members = JsonConvert.DeserializeObject<List<MemberDTO>>(json);
                return View(members);
            }
            ViewBag.Error = "Không lấy được danh sách thành viên!";
            return View(new List<CategoryDTO>());
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MemberDTO member)
        {
            var json = JsonConvert.SerializeObject(member);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("members", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Error = "Không thêm được thành viên!";
            return View(member);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var response = await _client.GetAsync($"members/{id}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var member = JsonConvert.DeserializeObject<MemberDTO>(json);
            return View(member);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MemberDTO member)
        {
            var json = JsonConvert.SerializeObject(member);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PutAsync("members", content);
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Error = "Cập nhật thất bại!";
            return View(member);
        }

        public async Task<IActionResult> Details(int id)
        {
            var response = await _client.GetAsync($"members/{id}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var member = JsonConvert.DeserializeObject<MemberDTO>(json);
            return View(member);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var response = await _client.GetAsync($"members/{id}");
            if (!response.IsSuccessStatusCode)
                return NotFound();

            var json = await response.Content.ReadAsStringAsync();
            var member = JsonConvert.DeserializeObject<MemberDTO>(json);
            return View(member);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var response = await _client.DeleteAsync($"members/{id}");
            if (response.IsSuccessStatusCode)
            {
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Error = "Xóa thất bại!";
            return RedirectToAction(nameof(Index));
        }
    }
}
