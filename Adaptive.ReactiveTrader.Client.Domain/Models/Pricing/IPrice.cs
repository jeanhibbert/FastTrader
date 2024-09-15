﻿using System;
using NewWave.FastTrader.Client.Domain.Models.ReferenceData;

namespace NewWave.FastTrader.Client.Domain.Models.Pricing
{
    public interface IPrice
    {
        IExecutablePrice Bid { get; }
        IExecutablePrice Ask { get; }
        decimal Mid { get; }
        ICurrencyPair CurrencyPair { get; }
        long QuoteId { get; }
        DateTime ValueDate { get; }
        decimal Spread { get; }
        bool IsStale { get; }
    }
}
