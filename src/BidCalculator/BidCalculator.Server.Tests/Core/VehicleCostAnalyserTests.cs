using BidCalculator.Server.Core;
using FluentAssertions;
using Xunit;

namespace BidCalculator.Server.Tests.Core;

public class VehicleCostAnalyserTests
{
	public class AnalyseMethod
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
		public void ReturnsExpectedCost_WhenUsingStandardApplicableFees(double basePrice, VehicleType type, double basicFee, double sellerFee, double associationFee, double storageFee, double total)
		{
			IEnumerable<IFeeCalculator> applicableFees = [
				new BasicFeeCalculator(),
				new SellerFeeCalculator(),
				new AssociationFeeCalculator(),
				new StorageFeeCalculator()
			];

			var calculator = new VehicleCostAnalyser();

			var result = calculator.Analyse(new VehicleBid { BasePrice = basePrice, Type = type }, applicableFees);

			result.Fees.OfType<BasicFee>().Single().Amount.Should().BeApproximately(basicFee, AmountPrecision);
			result.Fees.OfType<SellerFee>().Single().Amount.Should().BeApproximately(sellerFee, AmountPrecision);
			result.Fees.OfType<AssociationFee>().Single().Amount.Should().BeApproximately(associationFee, AmountPrecision);
			result.Fees.OfType<StorageFee>().Single().Amount.Should().BeApproximately(storageFee, AmountPrecision);
			result.Total.Should().BeApproximately(total, AmountPrecision);
		}
	}
}