using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace Client;

public interface IPaymentService
{
    Task CheckHealth();
    Task<PaymentRequest> PerformPayment(string? reference, int amount, Guid merchantId);
}

public class PaymentService : IPaymentService
{
    private readonly HttpClient _httpClient;

    public PaymentService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri("https://test.rivr.io/api/public/");
        _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "add-your-key-here");
    }

    public async Task CheckHealth()
    {
        var response = await _httpClient.GetAsync("health");
        response.EnsureSuccessStatusCode();
    }

    public async Task<PaymentRequest> PerformPayment(string? reference, int amount, Guid merchantId)
    {
        var createPaymentRequest = new
        {
            Id = Guid.NewGuid(),
            MerchantId = merchantId,
            Reference = reference,
            Amount = amount,
            CallbackUrl = "https://my-url/callback"
        };
        var response = await _httpClient.PutAsJsonAsync($"payment-requests/{createPaymentRequest.Id}", createPaymentRequest);

        response.EnsureSuccessStatusCode();

        return await response.Content.ReadFromJsonAsync<PaymentRequest>() ?? throw new Exception("Could not deserialise the result into the expected type");
    }
}