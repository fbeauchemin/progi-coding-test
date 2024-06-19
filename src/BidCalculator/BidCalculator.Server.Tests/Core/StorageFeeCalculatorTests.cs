using BidCalculator.Server.Core;
using FluentAssertions;
using Xunit;

namespace BidCalculator.Server.Tests.Core;

public class StorageFeeCalculatorTests
{
	public class CalculateMethod
	{
		[Fact]
		public void ReturnsFixedAmountOf_100()
		{
			var calculator = new StorageFeeCalculator();

			var result = calculator.Calculate(new VehicleBid());

			result.Amount.Should().Be(100);
		}
	}
}