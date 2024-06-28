namespace BidCalculator.Server.Core;

public class AssociationFee(decimal amount) : Fee(amount)
{
    public override string Name => "Association";
}