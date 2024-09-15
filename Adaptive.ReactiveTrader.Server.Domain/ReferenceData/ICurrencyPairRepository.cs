using System.Collections.Generic;
using NewWave.FastTrader.Shared.DTO.ReferenceData;

namespace NewWave.FastTrader.Server.ReferenceData
{
    public interface ICurrencyPairRepository
    {
        IEnumerable<CurrencyPairInfo> GetAllCurrencyPairs();
        CurrencyPairDto GetCurrencyPair(string symbol);
        bool Exists(string symbol);
        decimal GetSampleRate(string symbol);
        IEnumerable<CurrencyPairInfo> GetAllCurrencyPairInfos();
    }
}