using Microsoft.AspNetCore.Mvc;

namespace Instalment.Controllers;

[ApiController]
public class CallbackController : ControllerBase
{
    private readonly ILogger<CallbackController> _logger;

    public CallbackController(ILogger<CallbackController> logger)
    {
        _logger = logger;
    }

    [HttpPost("callback")]
    public Task Callback([FromBody] CallbackRequest request)
    {
        _logger.LogInformation($"Received callback for instalment: {request.InstalmentId}. Status: {request.Status}");
        
        return Task.CompletedTask;
    }
}

public class CallbackRequest
{
    public Guid InstalmentId { get; set; }
    public InstalmentStatus Status { get; set; }
}

public enum InstalmentStatus
{
    Created = 1 << 0,
    Submitted = 1 << 1,
    Pending = 1 << 2,
    Granted = 1 << 3,
    Denied = 1 << 4,
    PaidOut = 1 << 5,
    AgreementSigned = 1 << 6,
    Expired = 1 << 7,
    PayedOut = 1 << 5,
}