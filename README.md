# Progi - The Bid Calcuation Test

Implementation of the Bid Calculation problem using C# and Vue.js.

## Prerequisites

- .Net Core 8
- Node.js

## Structure

This is a client / server application, where the client `bidcalculator.client` talks to the server `BidCalculator.Server` through a REST api.

### Frontend

The `bidcalculator.client` project is built using Vue.js. Tests are executed using Vitest.

### Backend

The `BidCalculator.Server` project is built using C# and Asp.Net Core 8. Test are located in the `BidCalculator.Server.Tests` project.


## Usage

To launch the app:

`$> dotnet run --project .\src\BidCalculator\BidCalculator.Server\BidCalculator.Server.csproj`

## Tests

To execute the backend tests:

`$ src\BidCalculator> dotnet test --collect:"XPlat Code Coverage" --results-directory:"temp"`

To generate code coverage report:

`$ src\BidCalculator> dotnet reportgenerator -reports:"temp\**\*.xml" -targetdir:"temp\coverage" -reporttype: html`

To execute the frontend tests:

- `$ src\BidCalculator\bidcalculator.client> npm install`
- `$ src\BidCalculator\bidcalculator.client> npm run test`
