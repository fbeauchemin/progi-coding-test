using BidCalculator.Server.Controllers;
using BidCalculator.Server.Core;
using FluentAssertions;
using Xunit;

namespace BidCalculator.Server.Tests.Controllers;

public class VehicleCostControllerTests
{
	public class IndexMethod
	{
		[Fact]
		public void IncludesBasicFee_AsApplicableFees()
		{
			var controller = new VehicleCostController();

			var result = controller.Index(new VehicleCostRequest());

			result.Value!.Fees.OfType<BasicFee>().Count().Should().Be(1);
		}

		[Fact]
		public void IncludesSellerFee_AsApplicableFees()
		{
			var controller = new VehicleCostController();

			var result = controller.Index(new VehicleCostRequest());

			result.Value!.Fees.OfType<SellerFee>().Count().Should().Be(1);
		}

		[Fact]
		public void IncludesAssociationFee_AsApplicableFees()
		{
			var controller = new VehicleCostController();

			var result = controller.Index(new VehicleCostRequest());

			result.Value!.Fees.OfType<AssociationFee>().Count().Should().Be(1);
		}

		[Fact]
		public void IncludesStorageFee_AsApplicableFees()
		{
			var controller = new VehicleCostController();

			var result = controller.Index(new VehicleCostRequest());

			result.Value!.Fees.OfType<StorageFee>().Count().Should().Be(1);
		}
	}
}