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
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJtZXJjaGFudC1pZCI6IjAwMDAwMDAwLTAwMDAtMDAwMC0wMDAwLTAwMDAwMDAwMDAwMCIsInBsYXRmb3JtLWlkIjoiODIxNWQwOWYtMjUwMC00OTVmLWEzZjMtYzJiNWYxNDVjZGRkIiwibmJmIjoxNjY3ODI0MjUwLCJleHAiOjE2OTkzNjAyNTAsImlhdCI6MTY2NzgyNDI1MH0.YVZVCKlOWMmvcc6RrfegVeS1AyyJkF2HH4HA2gTdbLc");
            _platformId = "8215d09f-2500-495f-a3f3-c2b5f145cddd";
        }

        public IActionResult Index()
        {
            return View();
        }

        public async Task<IActionResult> CreateMerchant()
        {
            var merchantId = Guid.NewGuid();

            var response =
                await _httpClient.PutAsJsonAsync($"platforms/{_platformId}/onboarding/{merchantId}", new
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
        /// <summary>
        /// URL that Rivr will use to notify caller about the outcome of the onboarding. The URL has to use HTTPS. Rivr will perform a HTTP POST with <see cref="Callback"/>
        /// </summary>
        /// <example>https://www.example.com/my/callback</example>
        public string? CallbackUrl { get; set; }
    }
    
    public class CreateMerchantResponse
    {
        public string? Snippet { get; set; }
    }
}