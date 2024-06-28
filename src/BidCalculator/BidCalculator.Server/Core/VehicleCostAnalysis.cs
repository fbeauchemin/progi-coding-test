namespace BidCalculator.Server.Core;

public class VehicleCostAnalysis(decimal basePrice, IEnumerable<Fee> applicableFees)
{
    public decimal BasePrice { get; } = basePrice;
    public IEnumerable<Fee> Fees { get; } = applicableFees;
    public decimal Total => Math.Round(BasePrice + Fees.Sum(f => f.Amount), 2);
}