using BidCalculator.Server.Core;
using Microsoft.AspNetCore.Mvc;

namespace BidCalculator.Server.Controllers;

[ApiController]
[Route("api/vehicle-cost")]
public class VehicleCostController : ControllerBase
{
	[HttpGet]
	[ProducesResponseType(StatusCodes.Status200OK)]
	public ActionResult<VehicleCostAnalysis> Index([FromQuery] VehicleCostRequest request)
	{
		var vehicleBid = new VehicleBid
		{
			BasePrice = request.BasePrice,
			Type = request.Type
		};

		IEnumerable<IFeeCalculator> applicableFees = [
			new BasicFeeCalculator(),
			new SellerFeeCalculator(),
			new AssociationFeeCalculator(),
			new StorageFeeCalculator()
		];

		var analyser = new VehicleCostAnalyser();

		var analysis = analyser.Analyse(vehicleBid, applicableFees);

		return analysis;
	}
}

public class VehicleCostRequest
{
	public double BasePrice { get; set; }
	public VehicleType Type { get; set; }
}