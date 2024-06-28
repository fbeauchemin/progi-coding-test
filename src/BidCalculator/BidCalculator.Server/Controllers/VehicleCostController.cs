using BidCalculator.Server.Core;
using Microsoft.AspNetCore.Mvc;

namespace BidCalculator.Server.Controllers;

[ApiController]
[Route("api/vehicle-cost")]
public class VehicleCostController(IVehicleCostAnalyser vehicleCostAnalyser, IEnumerable<IFeeCalculator> applicableFees)
    : ControllerBase
{
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    public ActionResult<VehicleCostAnalysis> Index([FromQuery] VehicleCostRequest request)
    {
        VehicleBid vehicleBid = new()
        {
            BasePrice = request.BasePrice,
            Type = request.Type
        };

        return vehicleCostAnalyser.Analyse(vehicleBid, applicableFees);
    }
}

public class VehicleCostRequest
{
    public decimal BasePrice { get; init; }
    public VehicleType Type { get; init; }
}