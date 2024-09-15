namespace NewWave.FastTrader.Server.Pricing
{
    public interface IPriceFeed
    {
        void Start();
        void SetUpdateFrequency(double value);
    }
}