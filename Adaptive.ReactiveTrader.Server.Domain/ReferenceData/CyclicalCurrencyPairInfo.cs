using System;
using System.Diagnostics;
using NewWave.FastTrader.Shared.DTO.Pricing;
using NewWave.FastTrader.Shared.DTO.ReferenceData;

namespace NewWave.FastTrader.Server.ReferenceData
{
    public sealed class CyclicalCurrencyPairInfo : CurrencyPairInfo
    {
        private readonly decimal _min;
        private readonly decimal _max;
        private bool _isIncreasing = false;

        public CyclicalCurrencyPairInfo(CurrencyPairDto currencyPair, decimal sampleRate, bool enabled, string comment, decimal min, decimal max)
            : base(currencyPair, sampleRate, enabled, comment)
        {
            _min = min;
            _max = max;
        }

        public override PriceDto GenerateNextQuote(PriceDto previousPrice)
        {
            var pow = (decimal)Math.Pow(10, CurrencyPair.RatePrecision);
            var movement = 5 / pow;

            decimal newMid;
            if (_isIncreasing)
            {
                newMid = previousPrice.Mid + movement;
                if (newMid > _max)
                    _isIncreasing = false;
            }
            else
            {
                newMid = previousPrice.Mid - movement;
                if (newMid < _min)
                    _isIncreasing = true;
            }

            return new PriceDto
            {
                Symbol = previousPrice.Symbol,
                QuoteId = previousPrice.QuoteId + 1,
                ValueDate = DateTime.UtcNow.AddDays(2).Date,
                Mid = newMid,
                Ask = newMid + 5 / pow,
                Bid = newMid - 5 / pow,
                CreationTimestamp = Stopwatch.GetTimestamp()
            };
        }
    }
}