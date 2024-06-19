namespace BidCalculator.Server.Core;

public class StorageFee(double amount) : Fee(amount)
{
	public override string Name => "Storage";
}