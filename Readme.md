# Baase Integration Example
This repository contains a basic integration example created with .NET Core Example. With this code, you should be able to get a basic understanding about the effort needed to integrate with Baase.

Please refer to the API documentation for details: https://one.baase.com/docs

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

Enter the information in the required places and run the code:

```
dotnet run --project .\src\Client\Client.csproj
dotnet run --project .\src\CallbackService\Client.csproj

