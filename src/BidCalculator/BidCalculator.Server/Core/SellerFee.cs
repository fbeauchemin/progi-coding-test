namespace BidCalculator.Server.Core;

public class SellerFee(double amount) : Fee(amount)
{
	public override string Name => "Seller";
}