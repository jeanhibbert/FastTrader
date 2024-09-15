using NewWave.FastTrader.Client.Domain.Models.ReferenceData;
using NewWave.FastTrader.Shared.DTO.Pricing;

namespace NewWave.FastTrader.Client.Domain.Models.Pricing
{
    internal interface IPriceFactory
    {
        IPrice Create(PriceDto price, ICurrencyPair currencyPair);
    }
}