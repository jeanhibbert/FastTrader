﻿using System.Collections.Generic;
using NewWave.FastTrader.Shared.DTO.ReferenceData;

namespace NewWave.FastTrader.Server.ReferenceData
{
    public class CurrencyPairRepository : ICurrencyPairRepository
    {
        private readonly Dictionary<string, CurrencyPairInfo> _currencyPairs = new Dictionary<string, CurrencyPairInfo>
        {
            {"EURUSD", CreateCycleCurrencyPairInfo("EURUSD", 4, 5, 1.3629m, true, 1.36m, 1.365m,"Price cycles between 1.36-1.365")},
            {"USDJPY", CreateCurrencyPairInfo("USDJPY", 2, 3, 102.14m, true)},
            {"GBPUSD", CreateCurrencyPairInfo("GBPUSD", 4, 5, 1.6395m, true, "Server waits 1.5sec to execute then sends a trade done.")},       // ExecutionService
            {"GBPJPY", CreateCurrencyPairInfo("GBPJPY", 2, 3, 167.67m, true, "Always rejects upon execution.")},                                // ExecutionService
            {"EURGBP", CreateCurrencyPairInfo("EURGBP", 4, 5, 0.8312m, false)},
            {"USDCHF", CreateCurrencyPairInfo("USDCHF", 4, 5, 0.897m, false)},
            {"EURJPY", CreateCurrencyPairInfo("EURJPY", 2, 3, 139.22m, true, "Waits 5sec before sending a trade response (times-out).")},       // ExecutionService
            {"EURCHF", CreateCurrencyPairInfo("EURCHF", 4, 5, 1.2224m, false, "Server waits before sending a first price update.")},            // PricingHub
            {"AUDUSD", CreateCurrencyPairInfo("AUDUSD", 4, 5, 0.8925m, false)},
            {"NZDUSD", CreateCurrencyPairInfo("NZDUSD", 4, 5, 0.8263m, false)},
            {"USDCAD", CreateCurrencyPairInfo("USDCAD", 4, 5, 1.1043m, false)},
            {"EURCAD", CreateCurrencyPairInfo("EURCAD", 4, 5, 1.5062m, false)},
            {"EURAUD", CreateCurrencyPairInfo("EURAUD", 4, 5, 1.5256m, false)},
            {"AUDCAD", CreateCurrencyPairInfo("AUDCAD", 4, 5, 0.9873m, false)},
            {"GBPCHF", CreateCurrencyPairInfo("GBPCHF", 4, 5, 1.4723m, false)},
            {"CHFJPY", CreateCurrencyPairInfo("CHFJPY", 2, 3, 113.8591m, false)},
            {"AUDJPY", CreateCurrencyPairInfo("AUDJPY", 2, 3, 91.3133m, false)},
            {"AUDNZD", CreateCurrencyPairInfo("AUDNZD", 4, 5, 1.0807m, false)},
            {"CADUSD", CreateCurrencyPairInfo("CADUSD", 4, 5, 0.9054m, false)},
            {"CADJPY", CreateCurrencyPairInfo("CADJPY", 2, 3, 92.4686m, false)},
            {"CHFUSD", CreateCurrencyPairInfo("CHFUSD", 4, 5, 1.1148m, false)},
            {"EURNOK", CreateCurrencyPairInfo("EURNOK", 4, 4, 8.3613m, false)},
            {"EURSEK", CreateCurrencyPairInfo("EURSEK", 4, 4, 8.8505m, false)},
        };

        private static CurrencyPairInfo CreateCurrencyPairInfo(string symbol, int pipsPosition, int ratePrecision, decimal sampleRate, bool enabled, string comment= "")
        {
            return new RandomWalkCurrencyPairInfo(new CurrencyPairDto(symbol, ratePrecision, pipsPosition), sampleRate, enabled, comment);
        }
        private static CurrencyPairInfo CreateCycleCurrencyPairInfo(string symbol, int pipsPosition, int ratePrecision, decimal sampleRate, bool enabled, decimal min, decimal max, string comment )
        {
            return new CyclicalCurrencyPairInfo(new CurrencyPairDto(symbol, ratePrecision, pipsPosition), sampleRate, enabled, comment, min, max);
        }

        public IEnumerable<CurrencyPairInfo> GetAllCurrencyPairs()
        {
            return _currencyPairs.Values;
        }

        public IEnumerable<CurrencyPairInfo> GetAllCurrencyPairInfos()
        {
            return _currencyPairs.Values;
        }

        public CurrencyPairDto GetCurrencyPair(string symbol)
        {
            return _currencyPairs[symbol].CurrencyPair;
        }

        public decimal GetSampleRate(string symbol)
        {
            return _currencyPairs[symbol].SampleRate;
        }

        public bool Exists(string symbol)
        {
            return _currencyPairs.ContainsKey(symbol);
        }
    }
}