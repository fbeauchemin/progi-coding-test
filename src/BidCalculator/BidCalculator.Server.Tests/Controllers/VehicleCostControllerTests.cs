using BidCalculator.Server.Controllers;
using BidCalculator.Server.Core;
using FluentAssertions;
using NSubstitute;
using PAP.NSubstitute.FluentAssertionsBridge;
using Xunit;

namespace BidCalculator.Server.Tests.Controllers;

public class VehicleCostControllerTests
{
    public class IndexMethod
    {
        [Fact]
        public void UsesCostRequest_ToRunCostAnalyser()
        {
            var request = new VehicleCostRequest { BasePrice = 100, Type = VehicleType.Luxury };

            var costAnalyser = Substitute.For<IVehicleCostAnalyser>();

            var controller = CreateController(costAnalyser);

            // Act
            _ = controller.Index(request);

            // Assert
            var expectedBid = new VehicleBid { BasePrice = request.BasePrice, Type = request.Type };
            costAnalyser.Received()
                .Analyse(
                    Verify.That<VehicleBid>(bid => bid.Should().BeEquivalentTo(expectedBid)),
                    Arg.Any<IEnumerable<IFeeCalculator>>()
                );
        }

        [Fact]
        public void UsesApplicableFees_ToRunCostAnalysis()
        {
            var costAnalyser = Substitute.For<IVehicleCostAnalyser>();
            var applicableFees = Array.Empty<IFeeCalculator>();
            var controller = CreateController(costAnalyser, applicableFees);

            // Act
            _ = controller.Index(new VehicleCostRequest());

            // Assert
            costAnalyser.Received()
                .Analyse(
                    Arg.Any<VehicleBid>(),
                    Verify.That<IEnumerable<IFeeCalculator>>(fees => fees.Should().BeEquivalentTo(applicableFees))
                );
        }

        [Fact]
        public void ReturnsCostAnalysis_FromCostAnalyser()
        {
            var costAnalysis = new VehicleCostAnalysis(0, []);

            var costAnalyser = Substitute.For<IVehicleCostAnalyser>();
            costAnalyser
                .Analyse(Arg.Any<VehicleBid>(), Arg.Any<IEnumerable<IFeeCalculator>>())
                .Returns(costAnalysis);

            var controller = CreateController(costAnalyser);

            // Act
            var result = controller.Index(new VehicleCostRequest());

            // Assert
            result.Value.Should().Be(costAnalysis);
        }


        private static VehicleCostController CreateController(IVehicleCostAnalyser costAnalyser)
        {
            return CreateController(costAnalyser, []);
        }

        private static VehicleCostController CreateController(IVehicleCostAnalyser costAnalyser, IEnumerable<IFeeCalculator> applicableFees)
        {
            return new VehicleCostController(costAnalyser, applicableFees);
        }
    }
}