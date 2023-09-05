namespace CallbackService.Controllers;

public class OnboardingCallbackModel
{
    public Guid? MerchantId { get; set; }
    public OnboardingState State { get; set; }
}

public enum OnboardingState
{
    Pending = 0,
    Timeout = 1,
    Failed = 2,
    Completed = 3,
}