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

    [HttpPost]
    public Task Callback([FromBody] CallbackModel callbackModel)
    {
        _logger.LogInformation($"Received callback for {callbackModel.PaymentRequestId}, state: {callbackModel.State}");

        return Task.CompletedTask;
    }
}