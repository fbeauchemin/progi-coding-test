namespace BidCalculator.Server.Core;

public class VehicleCostAnalysis(decimal basePrice, IEnumerable<Fee> applicableFees)
{
    public IEnumerable<Fee> Fees { get; } = applicableFees;
    public decimal Total => Math.Round(basePrice + Fees.Sum(f => f.Amount), 2);
}