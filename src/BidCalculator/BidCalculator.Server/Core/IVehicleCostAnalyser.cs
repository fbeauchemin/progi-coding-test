namespace BidCalculator.Server.Core;

public interface IVehicleCostAnalyser
{
    VehicleCostAnalysis Analyse(VehicleBid bid, IEnumerable<IFeeCalculator> applicableFees);
}