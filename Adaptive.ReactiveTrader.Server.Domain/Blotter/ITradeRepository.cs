using System.Collections.Generic;
using NewWave.FastTrader.Shared.DTO.Execution;

namespace NewWave.FastTrader.Server.Blotter
{
    public interface ITradeRepository
    {
        void StoreTrade(TradeDto trade);
        IList<TradeDto> GetAllTrades();
    }
}