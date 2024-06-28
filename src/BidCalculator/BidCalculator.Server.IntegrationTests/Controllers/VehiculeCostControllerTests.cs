using BidCalculator.Server.Core;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc.Testing;
using System.Net.Http.Json;
using Xunit;

namespace BidCalculator.Server.IntegrationTests.Controllers;

public class VehiculeCostControllerTests
{
    public class IndexMethod(WebApplicationFactory<Program> factory) : IClassFixture<WebApplicationFactory<Program>>
    {
        private const double AmountPrecision = 0.001;

        [Theory]
        [InlineData(398.00, VehicleType.Common, 39.80, 7.96, 5.00, 100.00, 550.76)]
        [InlineData(501.00, VehicleType.Common, 50.00, 10.02, 10.00, 100.00, 671.02)]
        [InlineData(57.00, VehicleType.Common, 10.00, 1.14, 5.00, 100.00, 173.14)]
        [InlineData(1_800.00, VehicleType.Luxury, 180.00, 72.00, 15.00, 100.00, 2_167.00)]
        [InlineData(1_100.00, VehicleType.Common, 50.00, 22.00, 15.00, 100.00, 1_287.00)]
        [InlineData(1_000_000.00, VehicleType.Luxury, 200.00, 40_000.00, 20.00, 100.00, 1_040_320.00)]
        [InlineData(157, VehicleType.Common, 15.70, 3.14, 5, 100.00, 280.84)]
        public async Task ReturnsSuccessAndCorrectData(double basePrice, VehicleType type, double basicFee, double sellerFee, double associationFee, double storageFee, double total)
        {
            var client = factory.CreateClient();

            var response = await client.GetAsync($"api/vehicle-cost?basePrice={basePrice}&type={type}");

            var result = await response.Content.ReadFromJsonAsync<CostResponse>();
            result!.Fees.Single(fee => fee.Name == "Basic").Amount.Should().BeApproximately(basicFee, AmountPrecision);
            result.Fees.Single(fee => fee.Name == "Seller").Amount.Should().BeApproximately(sellerFee, AmountPrecision);
            result.Fees.Single(fee => fee.Name == "Association").Amount.Should().BeApproximately(associationFee, AmountPrecision);
            result.Fees.Single(fee => fee.Name == "Storage").Amount.Should().BeApproximately(storageFee, AmountPrecision);
            result.Total.Should().BeApproximately(total, AmountPrecision);
        }
    }
}

public class CostResponse
{
    public double Total { get; set; }
    public IEnumerable<FeeItem> Fees { get; set; } = [];
    public class FeeItem
    {
        public string Name { get; set; } = string.Empty;
        public double Amount { get; set; }
    }
}