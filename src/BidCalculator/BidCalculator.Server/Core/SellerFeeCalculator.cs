namespace BidCalculator.Server.Core;

public class SellerFeeCalculator : IFeeCalculator
{
	private const double CommonPercentage = 2;
	private const double LuxuryPercentage = 4;

	public Fee Calculate(VehicleBid bid)
	{
		var percentage = bid.Type == VehicleType.Common ? CommonPercentage : LuxuryPercentage;
		var amount = bid.BasePrice * (percentage / 100);

		return new SellerFee(amount);
	}
}