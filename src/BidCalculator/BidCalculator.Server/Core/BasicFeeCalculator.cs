namespace BidCalculator.Server.Core;

public class BasicFeeCalculator : IFeeCalculator
{
    private const int CommonMinimum = 10;
    private const int CommonMaximum = 50;

    private const int LuxuryMinimum = 25;
    private const int LuxuryMaximum = 200;

    private const decimal FeePercentage = 10;

    public Fee Calculate(VehicleBid bid)
    {
        int minimum = bid.Type == VehicleType.Common ? CommonMinimum : LuxuryMinimum;
        int maximum = bid.Type == VehicleType.Common ? CommonMaximum : LuxuryMaximum;
        decimal amount = bid.BasePrice * (FeePercentage / 100);

        if (amount < minimum)
            amount = minimum;

        if (amount > maximum)
            amount = maximum;

        return new BasicFee(amount);
    }
}