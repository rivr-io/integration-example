using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Onboarding.Controllers
{
    public class HomeController : Controller
    {
        private readonly HttpClient _httpClient;
        private readonly string _platformId = "YOUR_PLATFORM_ID_HERE";
        private readonly string _platformApiKey = "YOUR_API_TOKEN_HERE";

        public HomeController(HttpClient httpClient)
        {
            _httpClient = httpClient;
            _httpClient.BaseAddress = new Uri("https://test.rivr.io/api/public/");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", _platformApiKey);
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
                    OrganisationNumber = "ORG_NUMBER",
                    CallbackUrl = "https://your-callback-url"
                });

            response.EnsureSuccessStatusCode();

            var responseAsJson = await response.Content.ReadFromJsonAsync<CreateMerchantResponse>();

            ViewBag.Snippet = responseAsJson?.Snippet;
            ViewBag.Link = responseAsJson?.Link;

            return View("Index");
        }
    }

    public class CreateMerchantRequest
    {
        /// <summary>
        /// Organisation number for the company that is being onboarded
        /// </summary>
        /// <example>1234567890</example>
        public string OrganisationNumber { get; set; } = null!;
        /// <summary>
        /// URL that Rivr will use to notify caller about the outcome of the onboarding. The URL has to use HTTPS. Rivr will perform a HTTP POST with <see cref="Callback"/>
        /// </summary>
        /// <example>https://www.example.com/my/callback</example>
        public string? CallbackUrl { get; set; }
    }
    
    public class CreateMerchantResponse
    {
        public string Snippet { get; set; } = null!;
        public string Link { get; set; } = null!;
    }
}