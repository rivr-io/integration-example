namespace CallbackService.Controllers;

public class InstalmentCallbackModel
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
}