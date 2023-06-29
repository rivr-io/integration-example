using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace Instalment.Controllers;

public class HomeController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly string _platformId = "YOUR_PLATFORM_ID_HERE";
    private readonly string _platformApiKey = "YOUR_API_TOKEN_HERE";
    private readonly string _merchantId = "YOUR_MERCHANT_ID_HERE";

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

    public async Task<IActionResult> InitiateInstalment()
    {
        var instalmentId = Guid.NewGuid();

        var response =
            await _httpClient.PutAsJsonAsync($"platforms/{_platformId}/instalments/{instalmentId}",
                new InitiateInstalmentRequest
                {
                    MerchantId = Guid.Parse(_merchantId),
                    Reference = "OCR-123456789",
                    Amount = 3000,
                    PersonalNumber = "9001010017",
                    CallbackUrl = "https://localhost:7265/callback"
                });

        response.EnsureSuccessStatusCode();

        var responseAsJson = await response.Content.ReadFromJsonAsync<InitiateInstalmentResponse>();

        ViewBag.Snippet = responseAsJson?.Snippet;
        ViewBag.Link = responseAsJson?.Link;

        return View("Index");
    }
}

public class InitiateInstalmentResponse
{
    public string Snippet { get; set; } = null!;
    public string Link { get; set; } = null!;
}

public class InitiateInstalmentRequest
{
    public Guid MerchantId { get; set; }
    public string? Reference { get; set; }
    public int Amount { get; set; }
    public string PersonalNumber { get; set; } = null!;
    public string? CallbackUrl { get; set; }
}