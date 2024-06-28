using BidCalculator.Server.Core;
using FluentAssertions;
using NSubstitute;
using Xunit;

namespace BidCalculator.Server.Tests.Core;

public class VehicleCostAnalyserTests
{
    public class AnalyseMethod
    {
        [Fact]
        public void AppliesAllFees()
        {
            VehicleBid vehicleBid = new();

            IFeeCalculator feeCalculator1 = Substitute.For<IFeeCalculator>();
            feeCalculator1.Calculate(vehicleBid).Returns(new TestFee(100));

            IFeeCalculator feeCalculator2 = Substitute.For<IFeeCalculator>();
            feeCalculator2.Calculate(vehicleBid).Returns(new TestFee(200));

            // Act
            VehicleCostAnalysis result = new VehicleCostAnalyser().Analyse(vehicleBid, [feeCalculator1, feeCalculator2]);

            result.Fees.ElementAt(0).Amount.Should().Be(100);
            result.Fees.ElementAt(1).Amount.Should().Be(200);
        }

        [Fact]
        public void UsesBasePriceFromBid_ToCompleteAnalysis()
        {
            VehicleBid vehicleBid = new() { BasePrice = 123 };

            VehicleCostAnalysis result = new VehicleCostAnalyser().Analyse(vehicleBid, []);

            result.Total.Should().Be(123);
        }
    }
}

public class TestFee(decimal amount) : Fee(amount)
{
    public override string Name => "Test";
}