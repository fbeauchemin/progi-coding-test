# Progi - The Bid Calculation Test

Implementation of the Bid Calculation problem using C# and Vue.js.

## Prerequisites

- .Net Core 8
- Node.js

## Structure

This is a client / server application, where the client `bidcalculator.client` talks to the server `BidCalculator.Server` through a REST api.

### Frontend

The `bidcalculator.client` project is built using Vue.js. Tests are executed using Vitest.

### Backend

The `BidCalculator.Server` project is built using C# and Asp.Net Core 8. Tests are located in the `BidCalculator.Server.Tests` project.


## Usage

To launch the app:

`$> dotnet run --project .\src\BidCalculator\BidCalculator.Server\BidCalculator.Server.csproj`

## Tests

To execute the backend tests:

`$> dotnet test .\src\BidCalculator\`

To execute the frontend tests:

- `$ src\BidCalculator\bidcalculator.client> npm install`
- `$ src\BidCalculator\bidcalculator.client> npm run test`
