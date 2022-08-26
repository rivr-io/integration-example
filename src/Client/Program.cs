using Client;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;


await Host
    .CreateDefaultBuilder()
    .ConfigureServices(serviceCollection =>
    {
        serviceCollection.AddHostedService<PaymentManager>();
        serviceCollection.AddHostedService<HealthChecker>();
        serviceCollection.AddHttpClient<IPaymentService, PaymentService>();
    })
    .RunConsoleAsync();