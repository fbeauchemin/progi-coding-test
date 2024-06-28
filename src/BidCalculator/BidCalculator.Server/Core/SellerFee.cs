namespace BidCalculator.Server.Core;

public class SellerFee(decimal amount) : Fee(amount)
{
    public override string Name => "Seller";
}