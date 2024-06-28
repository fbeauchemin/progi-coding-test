namespace BidCalculator.Server.Core;

public abstract class Fee(decimal amount)
{
    public abstract string Name { get; }
    public decimal Amount { get; set; } = Math.Round(amount, 2);
}