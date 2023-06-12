namespace CallbackService.Controllers;

public class PaymentCallbackModel
{
    public Guid PaymentRequestId { get; set; }
    public string State { get; set; }
}
