using Microsoft.Extensions.Hosting;

namespace Client;

public class PaymentManager : IHostedService
{
    private readonly IPaymentService _paymentService;
    private readonly Guid _merchantId = Guid.Parse("enter-your-merchantId");

    public PaymentManager(IPaymentService paymentService)
    {
        _paymentService = paymentService;
    }

    public async Task StartAsync(CancellationToken cancellationToken)
    {
        Console.WriteLine("Payment Gateway ready");

        Console.WriteLine("What is the payment reference?");
        var reference = Console.ReadLine();

        Console.WriteLine("How much do you want to charge?");
        var amountAsString = Console.ReadLine();
        var amount = int.Parse(amountAsString!);

        var result = await _paymentService.PerformPayment(reference, amount, _merchantId);
        
        Console.WriteLine($"Visit this link to continue {result.Url}");
    }


    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}