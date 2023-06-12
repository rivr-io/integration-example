using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Onboarding.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _platformId;

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://test.rivr.io/api/public/");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "YOUR_API_TOKEN_HERE");
            _platformId = "YOUR_PLATFORM_ID_HERE";
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateMerchant()
        {
            var merchantId = Guid.NewGuid();

            var response =
                await _httpClient.PutAsJsonAsync($"platforms/{_platformId}/onboarding/{merchantId}", new CreateMerchantRequest
                {
                    CallbackUrl = "https://your-callback-url"
                });

            response.EnsureSuccessStatusCode();

            var responseAsJson = await response.Content.ReadFromJsonAsync<CreateMerchantResponse>();

            ViewBag.Snippet = responseAsJson?.Snippet;

            return View("Index");
        }
    }

    public class CreateMerchantRequest
    {
        public string? CallbackUrl { get; set; }
    }
    
    public class CreateMerchantResponse
    {
        public string? Snippet { get; set; }
    }
}