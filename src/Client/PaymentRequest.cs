﻿using System.Text.Json.Serialization;

namespace Client;

/// <summary>
/// The Payment Request Object is used in all payment request operations and the provided data object (body) should be in JSON format. 
/// </summary>
public class PaymentRequest
{
    /// <summary>
    /// Payment request ID (UUID). This is the same ID used as Path parameter.
    /// </summary>
    /// <example>497f6eca-6276-4993-bfeb-53cbbbba6f08</example>
    public Guid Id { get; set; }

    /// <summary>
    /// The unique identifier (UUID) of the Merchant (available in the Merchant portal)
    /// </summary>
    /// <example>c3073b9d-edd0-49f2-a28d-b7ded8ff9a8b</example>
    public Guid MerchantId { get; set; }

    /// <summary>
    /// A unique payment reference. This should be your internally unique payment reference (ie an order number or other reference).
    /// </summary>
    /// <example>order-1234</example>
    public string Reference { get; set; } = null!;

    /// <summary>
    /// The amount of money to pay. This amount has to be more than 0 and less than 100,000,000. Decimals are not supported. The currency is always SEK.
    /// </summary>
    /// <example>42000</example>
    public int Amount { get; set; }

    /// <summary>
    /// URL that Baase will use to notify caller about the outcome of the <see cref="PaymentRequest"/>. The URL has to use HTTPS. Baase will perform a HTTP POST with <see cref="Callback"/>
    /// </summary>
    /// <example>https://www.example.com/my/callback</example>
    public string? CallbackUrl { get; set; }

    /// <summary>
    /// The URL to the customer payment flow. This is generated by Baase and is retrievable through the GET operation: <code>GET /api/public/payment-requests/{paymentRequestId}</code>
    /// </summary>
    /// <example>Generated by Baase</example>
    public string? Url { get; set; }

    /// <summary>
    /// The URL as a QR Code image (Base64 encoded). This is generated by Baase and is retrievable through the GET operation: <code>GET /api/public/payment-requests/{paymentRequestId}</code>
    /// </summary>
    /// <example>Generated by Baase</example>
    public string? QrCode { get; set; }

    /// <summary>
    /// Payment Request state. 
    /// </summary>
    /// <example>Created</example>
    [JsonConverter(typeof(JsonStringEnumConverter))]
    public PaymentRequestState State { get; set; }
}