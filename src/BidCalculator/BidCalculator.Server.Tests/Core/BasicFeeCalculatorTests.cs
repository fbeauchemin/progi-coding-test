using BidCalculator.Server.Core;
using FluentAssertions;
using Xunit;

namespace BidCalculator.Server.Tests.Core
{
	public class BasicFeeCalculatorTests
	{
		public class CalculateMethod
		{
			[Theory]
			[InlineData(0, VehicleType.Common, 10)]
			[InlineData(37, VehicleType.Common, 10)]
			[InlineData(100, VehicleType.Common, 10)]
			[InlineData(151, VehicleType.Common, 15.10)]
			[InlineData(225, VehicleType.Common, 22.5)]
			[InlineData(400, VehicleType.Common, 40)]
			[InlineData(500, VehicleType.Common, 50)]
			[InlineData(789, VehicleType.Common, 50)]
			[InlineData(0, VehicleType.Luxury, 25)]
			[InlineData(123, VehicleType.Luxury, 25)]
			[InlineData(250, VehicleType.Luxury, 25)]
			[InlineData(654, VehicleType.Luxury, 65.4)]
			[InlineData(2000, VehicleType.Luxury, 200)]
			[InlineData(4536, VehicleType.Luxury, 200)]
			public void Returns_10_Percent_Of_BasePrice_AsFee_WithMinimumsAndMaximums(double basePrice, VehicleType type, double amount)
			{
				var calculator = new BasicFeeCalculator();

				var result = calculator.Calculate(new VehicleBid { BasePrice = basePrice, Type = type });

				result.Amount.Should().Be(amount);
			}
		}
	}
}
