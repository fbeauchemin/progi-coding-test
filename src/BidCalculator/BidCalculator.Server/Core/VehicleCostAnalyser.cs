namespace BidCalculator.Server.Core;

public class VehicleCostAnalyser : IVehicleCostAnalyser
{
    public VehicleCostAnalysis Analyse(VehicleBid bid, IEnumerable<IFeeCalculator> applicableFees)
    {
        List<Fee> fees = applicableFees.Select(fee => fee.Calculate(bid)).ToList();

        return new VehicleCostAnalysis(bid.BasePrice, fees);
    }
}