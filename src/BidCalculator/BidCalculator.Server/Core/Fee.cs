namespace BidCalculator.Server.Core;

public abstract class Fee(double amount)
{
	public abstract string Name { get; }
	public double Amount { get; set; } = Math.Round(amount, 2);
}