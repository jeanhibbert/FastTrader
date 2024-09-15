using NewWave.FastTrader.Shared.DTO.ReferenceData;

namespace NewWave.FastTrader.Client.Domain.Models.ReferenceData
{
    internal interface ICurrencyPairUpdateFactory
    {
        ICurrencyPairUpdate Create(CurrencyPairUpdateDto currencyPairUpdate);
    }
}