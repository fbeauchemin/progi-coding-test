using BidCalculator.Server.Core;
using FluentAssertions;
using Xunit;

namespace BidCalculator.Server.Tests.Core;

public class AssociationFeeCalculatorTests
{
    public class CalculateMethod
    {
        [Theory]
        [InlineData(1)]
        [InlineData(34)]
        [InlineData(412)]
        [InlineData(500)]
        public void Returns_Amount_Of_5_WhenBasePriceIsBetween_1_and_500(decimal basePrice)
        {
            var calculator = new AssociationFeeCalculator();

            var result = calculator.Calculate(new VehicleBid { BasePrice = basePrice, Type = VehicleType.Common });

            result.Amount.Should().Be(5);
        }

        [Theory]
        [InlineData(501)]
        [InlineData(678)]
        [InlineData(834)]
        [InlineData(1000)]
        public void Returns_Amount_Of_10_WhenBasePriceIsGreaterThan_500_Up_To_1000(decimal basePrice)
        {
            var calculator = new AssociationFeeCalculator();

            var result = calculator.Calculate(new VehicleBid { BasePrice = basePrice, Type = VehicleType.Common });

            result.Amount.Should().Be(10);
        }

        [Theory]
        [InlineData(1001)]
        [InlineData(1344)]
        [InlineData(2587)]
        [InlineData(3000)]
        public void Returns_Amount_Of_15_WhenBasePriceIsGreaterThan_1000_Up_To_3000(decimal basePrice)
        {
            var calculator = new AssociationFeeCalculator();

            var result = calculator.Calculate(new VehicleBid { BasePrice = basePrice, Type = VehicleType.Common });

            result.Amount.Should().Be(15);
        }

        [Theory]
        [InlineData(3001)]
        [InlineData(4544)]
        [InlineData(7887)]
        [InlineData(1324300)]
        public void Returns_Amount_Of_30_WhenBasePriceIsOver_3000(decimal basePrice)
        {
            var calculator = new AssociationFeeCalculator();

            var result = calculator.Calculate(new VehicleBid { BasePrice = basePrice, Type = VehicleType.Common });

            result.Amount.Should().Be(20);
        }

        [Theory]
        [InlineData(-1)]
        [InlineData(-12.5)]
        [InlineData(-120)]
        [InlineData(-3425)]
        public void Returns_Amount_Of_0_WhenBasePriceIsLowerThan_0(decimal basePrice)
        {
            var calculator = new AssociationFeeCalculator();

            var result = calculator.Calculate(new VehicleBid { BasePrice = basePrice, Type = VehicleType.Common });

            result.Amount.Should().Be(0);
        }
    }
}