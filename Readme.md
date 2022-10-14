# Baase Integration Example
This repository contains a basic integration example created with .NET Core Example. With this code, you should be able to get a basic understanding about the effort needed to integrate with Baase.

Please refer to the API documentation for details: https://www.rivr.io/docs

## API Methods covered
This solution covers the creation of a *PaymentRequest* and how to call the *Health* endpoint as well.

These are the methods covered: 
- PUT /payment-requests
- GET /health

## Getting started
Clone the repository and use your preferred .NET Core development suite to build the solution.

Before you run the solution you need to obtain the following information: 
- Merchant ID
- Merchant API Key
- Callback URL (leave empty if you don't want to test callbacks)

Enter the information in the required places and run the code:

```
dotnet run --project .\src\Client\Client.csproj
dotnet run --project .\src\CallbackService\Client.csproj
```

## Workflow

This example integration creates an instance of an API Client which is used by two hosted services. One is a simple Console dialog which you may use to create *PaymentRequests*. If there is a callback URL supplied in the request, the callback should be called immediately with the supplied PaymentRequestId and State: Created. There is a URL returned when creating a PaymentRequest and that URL points to the consumer flow. When in a Baase hosted test environment (ie test.baase.com) all third-party integrations are simulated and one should feel free to go through the flow as a consumer. Receiving loan offers and accepting loans should also trigger callbacks. Since the integrations are simulated, there will never be any state changes beyond Accepted.

## Support

Please refer to [www.rivr.io](https://www.rivr.io) for support issues.