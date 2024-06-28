using BidCalculator.Server.Core;
using FluentAssertions;
using Xunit;

namespace BidCalculator.Server.Tests.Core;

public class SellerFeeCalculatorTests
{
    public class CalculateMethod
    {
        [Theory]
        [InlineData(0, 0)]
        [InlineData(10, 0.2)]
        [InlineData(15, 0.3)]
        [InlineData(45, 0.9)]
        [InlineData(50, 1)]
        [InlineData(876, 17.52)]
        public void Returns_2_Percent_Of_BasePrice_ForCommonType(decimal basePrice, decimal amount)
        {
            var calculator = new SellerFeeCalculator();

            var result = calculator.Calculate(new VehicleBid { BasePrice = basePrice, Type = VehicleType.Common });

            result.Amount.Should().Be(amount);
        }

        [Theory]
        [InlineData(0, 0)]
        [InlineData(10, 0.4)]
        [InlineData(15, 0.6)]
        [InlineData(45, 1.8)]
        [InlineData(50, 2)]
        [InlineData(876, 35.04)]
        public void Returns_4_Percent_Of_BasePrice_ForLuxuryType(decimal basePrice, decimal amount)
        {
            var calculator = new SellerFeeCalculator();

            var result = calculator.Calculate(new VehicleBid { BasePrice = basePrice, Type = VehicleType.Luxury });

            result.Amount.Should().Be(amount);
        }
    }
}