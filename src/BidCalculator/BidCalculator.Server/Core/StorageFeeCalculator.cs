namespace BidCalculator.Server.Core;

public class StorageFeeCalculator : IFeeCalculator
{
	public Fee Calculate(VehicleBid bid)
	{
		return new StorageFee(100);
	}
}