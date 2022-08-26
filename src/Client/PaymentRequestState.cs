namespace Client;

/// <summary>
/// The states a Payment Request can be in
/// </summary>
public enum PaymentRequestState
{
    Created,
    Submitted,
    OfferReceived,
    Accepted,
    Denied,
    Undisclosed
}