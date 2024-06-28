using BidCalculator.Server.Core;
using FluentAssertions;
using Xunit;

namespace BidCalculator.Server.Tests.Core;

public class VehicleCostAnalysisTests
{
    public class TotalProperty
    {
        [Fact]
        public void ReturnsBasePrice_WhenNoFeesAreApplied()
        {
            const decimal basePrice = 123.45m;

            VehicleCostAnalysis analysis = new(basePrice, []);

            analysis.Total.Should().Be(basePrice);
        }

        [Theory]
        [InlineData(1.000, 1.00)]
        [InlineData(1.005, 1.00)]
        [InlineData(1.0051, 1.01)]
        [InlineData(1.007, 1.01)]
        [InlineData(1.0048, 1.00)]
        public void RoundsResultTo_2_Digits(decimal basePrice, decimal expectedTotal)
        {
            VehicleCostAnalysis analysis = new(basePrice, []);

            analysis.Total.Should().Be(expectedTotal);
        }


        public static TheoryData<decimal, decimal[], decimal> AddsAllFeesToBasePriceData =>
            new()
            {
                { 1, [], 1 },
                { 1, [2, 3, 4], 10 },
                { 1.32m, [2.345m, 45.2m, 8.67m], 57.53m}
            };

        [Theory]
        [MemberData(nameof(AddsAllFeesToBasePriceData))]
        public void AddsAllFeesToBasePrice(decimal basePrice, decimal[] amounts, decimal expectedTotal)
        {
            IEnumerable<BasicFee> fees = amounts.Select(amount => new BasicFee(amount));

            VehicleCostAnalysis analysis = new(basePrice, fees);

            analysis.Total.Should().Be(expectedTotal);
        }
    }
}