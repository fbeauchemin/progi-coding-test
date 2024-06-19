namespace BidCalculator.Server.Core;

public interface IFeeCalculator
{
	Fee Calculate(VehicleBid bid);
}