using Microsoft.Extensions.Hosting;

namespace Client;

public class HealthChecker : IHostedService
{
    private readonly IPaymentService _paymentService;

    public HealthChecker(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        await PerformHealthCheck();
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }

    private async Task PerformHealthCheck()
    {
        await _paymentService.CheckHealth();
        Console.WriteLine("Performed health check.");
        await Task.Delay(60 * 1000);
        await PerformHealthCheck();
    }
}