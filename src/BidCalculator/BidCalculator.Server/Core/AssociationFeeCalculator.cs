namespace BidCalculator.Server.Core;

public class AssociationFeeCalculator : IFeeCalculator
{
	public Fee Calculate(VehicleBid bid)
	{
		var amount = bid.BasePrice switch
		{
			> 0 and <= 500 => 5,
			> 500 and <= 1000 => 10,
			> 1000 and <= 3000 => 15,
			> 3000 => 20,
			_ => 0
		};

		return new AssociationFee(amount);
	}
}