using BusinessObject.DTO;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Text;

namespace eStoreClient.Controllers
{
    public class LoginController : Controller
    {
        private readonly HttpClient _client;

        public LoginController(IConfiguration config)
        {
            var baseUrl = config["ApiSettings:BaseUrl"];
            _client = new HttpClient { BaseAddress = new Uri(baseUrl) };
        }

        public IActionResult Index() => View("Login");

        [HttpPost]
        public async Task<IActionResult> Index(LoginDTO login)
        {
            var json = JsonConvert.SerializeObject(login);
            var content = new StringContent(json, Encoding.UTF8, "application/json");

            var response = await _client.PostAsync("members/login", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                dynamic data = JsonConvert.DeserializeObject(result);

                string role = data.role;
                if (role == "Admin")
                {
                    HttpContext.Session.SetString("Role", "Admin");
                    return RedirectToAction("Index", "Product");
                }
                else if (role == "Member")
                {
                    HttpContext.Session.SetString("Role", "Member");
                    HttpContext.Session.SetInt32("MemberId", (int)data.memberId);
                    return RedirectToAction("Profile", "Member");
                }
            }

            ViewBag.Error = "Sai email hoặc mật khẩu!";
            return View("Login", login);
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear(); 
            return RedirectToAction("Index", "Login");
        }

    }
}
