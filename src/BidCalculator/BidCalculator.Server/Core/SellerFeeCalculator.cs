namespace BidCalculator.Server.Core;

public class SellerFeeCalculator : IFeeCalculator
{
    private const decimal CommonPercentage = 2;
    private const decimal LuxuryPercentage = 4;

    public Fee Calculate(VehicleBid bid)
    {
        decimal percentage = bid.Type == VehicleType.Common ? CommonPercentage : LuxuryPercentage;
        decimal amount = bid.BasePrice * (percentage / 100);

        return new SellerFee(amount);
    }
}