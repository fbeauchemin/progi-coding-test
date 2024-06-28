namespace BidCalculator.Server.Core;

public class StorageFee(decimal amount) : Fee(amount)
{
    public override string Name => "Storage";
}