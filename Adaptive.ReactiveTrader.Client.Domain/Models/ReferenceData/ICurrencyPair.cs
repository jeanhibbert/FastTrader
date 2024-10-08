﻿using System;
using NewWave.FastTrader.Client.Domain.Models.Pricing;

namespace NewWave.FastTrader.Client.Domain.Models.ReferenceData
{
    public interface ICurrencyPair
    {
        string Symbol { get; }
        IObservable<IPrice> PriceStream { get; }
        int RatePrecision { get; }
        int PipsPosition { get; }
        string BaseCurrency { get; }
        string CounterCurrency { get; }
    }
}
