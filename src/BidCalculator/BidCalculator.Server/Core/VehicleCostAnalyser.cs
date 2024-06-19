namespace BidCalculator.Server.Core;

public class VehicleCostAnalyser
{
	public VehicleCostAnalysis Analyse(VehicleBid bid, IEnumerable<IFeeCalculator> applicableFees)
	{
		var fees = applicableFees.Select(fee => fee.Calculate(bid)).ToList();

		return new VehicleCostAnalysis(bid.BasePrice, fees);
	}
}