using System;
using NewWave.FastTrader.Client.Domain.Models.Pricing;
using NewWave.FastTrader.Client.Domain.Models.ReferenceData;

namespace NewWave.FastTrader.Client.Domain.Repositories
{
    interface IPriceRepository
    {
        IObservable<IPrice> GetPriceStream(ICurrencyPair currencyPair);
    }
}