using System.Net.Http.Headers;
using Microsoft.AspNetCore.Mvc;

namespace Instalment.Controllers;

public class HomeController : Controller
{
    private readonly HttpClient _httpClient;
    private readonly string _platformId = "PLATFORM_ID";
    private readonly string _platformApiKey = "API_TOKEN";
    private readonly string _merchantId = "MERCHANT_ID";

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
                    AccountNumber = "123-4567",
                    PersonalNumber = "9001010017",
                    CallbackUrl = "https://your-callback-url"
                });

        response.EnsureSuccessStatusCode();

        var responseAsJson = await response.Content.ReadFromJsonAsync<InitiateInstalmentResponse>();

        ViewBag.Snippet = responseAsJson?.Snippet;

        return View("Index");
    }
}

public class InitiateInstalmentResponse
{
    public string Snippet { get; set; } = null!;
}

public class InitiateInstalmentRequest
{
    /// <summary>
    /// the unique identifier (at Rivr) for the merchant that the instalment and invoice is linked to. This identifier is provided when creating a new merchant at rivr and should be stored for later use
    /// </summary>
    public Guid MerchantId { get; set; }
    /// <summary>
    /// OCR number for the original invoice. Used when creating outgoing payments.
    /// </summary>
    /// <example>01234567890123456789</example>
    public string? Reference { get; set; }
    /// <summary>
    /// The total amount that the instalment request should be created from
    /// </summary>
    /// <example>3500</example>
    public int Amount { get; set; }

    /// <summary>
    /// The swedish personal identity number of the end user
    /// </summary>
    /// <example>199001010017</example>
    public string PersonalNumber { get; set; } = null!;
    /// <summary>
    /// Account\Giro number that should be used for outgoing payment when the instalment is signed
    /// </summary>
    /// <example>1234-5678</example>
    public string? AccountNumber { get; set; }

    /// <summary>
    /// URL that Rivr will use to notify caller about the outcome of the instalment. The URL has to use HTTPS. Rivr will perform a HTTP POST with <see cref="Callback"/>
    /// </summary>
    /// <example>https://www.example.com/my/callback</example>
    public string? CallbackUrl { get; set; }
}