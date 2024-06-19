namespace BidCalculator.Server.Core;

public class BasicFee(double amount) : Fee(amount)
{
	public override string Name => "Basic";
}