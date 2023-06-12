using Instalment.Controllers;
using Microsoft.AspNetCore.Mvc;

namespace CallbackService.Controllers;

[ApiController]
[Route("[controller]")]
public class CallbackController : ControllerBase
{
    private readonly ILogger<CallbackController> _logger;

    public CallbackController(ILogger<CallbackController> logger)
    {
        _logger = logger;
    }

    [HttpPost("payment")]
    public Task PaymentCallback([FromBody] PaymentCallbackModel callbackModel)
    {
        _logger.LogInformation($"Received callback for {callbackModel.PaymentRequestId}, state: {callbackModel.State}");

        return Task.CompletedTask;
    }

    [HttpPost("onboarding")]
    public Task OnboardingCallback([FromBody] OnboardingCallbackModel callbackModel)
    {
        _logger.LogInformation($"Received callback for {callbackModel.MerchantId}, state: {callbackModel.State}");

        return Task.CompletedTask;
    }

    [HttpPost("instalment")]
    public Task Callback([FromBody] InstalmentCallbackModel callbackModel)
    {
        _logger.LogInformation($"Received callback for instalment: {callbackModel.InstalmentId}. Status: {callbackModel.Status}");

        return Task.CompletedTask;
    }
}