using NewWave.FastTrader.Shared.DTO.Pricing;
using NewWave.FastTrader.Shared.DTO.ReferenceData;

namespace NewWave.FastTrader.Server.ReferenceData
{
    public abstract class CurrencyPairInfo
    {
        public CurrencyPairDto CurrencyPair { get; private set; }
        public decimal SampleRate { get; private set; }
        public bool Enabled { get; set; }
        public string Comment { get; set; }
        public bool Stale { get; set; }

        protected CurrencyPairInfo(CurrencyPairDto currencyPair, decimal sampleRate, bool enabled, string comment)
        {
            CurrencyPair = currencyPair;
            SampleRate = sampleRate;
            Enabled = enabled;
            Comment = comment;
        }

        public abstract PriceDto GenerateNextQuote(PriceDto lastPrice);
    }
}