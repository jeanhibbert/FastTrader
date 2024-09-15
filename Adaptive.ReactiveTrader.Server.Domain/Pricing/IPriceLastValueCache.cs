using NewWave.FastTrader.Shared.DTO.Pricing;

namespace NewWave.FastTrader.Server.Pricing
{
    public interface IPriceLastValueCache
    {
        PriceDto GetLastValue(string currencyPair);
        void StoreLastValue(PriceDto price);
    }
}