namespace BidCalculator.Server.Core;

public class BasicFee(decimal amount) : Fee(amount)
{
    public override string Name => "Basic";
}