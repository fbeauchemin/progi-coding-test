namespace BidCalculator.Server.Core;

public class VehicleCostAnalysis (double basePrice, IEnumerable<Fee> applicableFees)
{
	public IEnumerable<Fee> Fees { get; } = applicableFees;
	public double Total => Math.Round(basePrice + Fees.Sum(f => f.Amount), 2);
}