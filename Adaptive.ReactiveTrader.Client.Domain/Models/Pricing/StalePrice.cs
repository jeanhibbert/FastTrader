﻿using System;
using NewWave.FastTrader.Client.Domain.Models.ReferenceData;

namespace NewWave.FastTrader.Client.Domain.Models.Pricing
{
    internal class StalePrice : IPrice
    {
        public StalePrice(ICurrencyPair currencyPair)
        {
            CurrencyPair = currencyPair;
        }

        public IExecutablePrice Bid
        {
            get { throw new InvalidOperationException("Should not access this property on stale price."); }
        }

        public IExecutablePrice Ask
        {
            get { throw new InvalidOperationException("Should not access this property on stale price."); }
        }

        public decimal Mid
        {
            get { throw new InvalidOperationException("Should not access this property on stale price."); }
        }

        public ICurrencyPair CurrencyPair { get; private set; }

        public long QuoteId
        {
            get { throw new InvalidOperationException("Should not access this property on stale price."); }
        }

        public DateTime ValueDate
        {
            get { throw new InvalidOperationException("Should not access this property on stale price."); }
        }

        public decimal Spread
        {
            get { throw new InvalidOperationException("Should not access this property on stale price."); }
        }

        public bool IsStale
        {
            get { return true; }
        }

        public TimeSpan ElpasedTimeSinceCreated
        {
            get { return TimeSpan.Zero; }
        }

        public override string ToString()
        {
            return string.Format("[{0}]: Stale", CurrencyPair.Symbol);
        }
    }
}