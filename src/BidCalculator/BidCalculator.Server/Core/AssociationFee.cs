namespace BidCalculator.Server.Core;

public class AssociationFee(double amount) : Fee(amount)
{
	public override string Name => "Association";
}