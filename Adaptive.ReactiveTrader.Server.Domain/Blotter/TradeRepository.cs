using System.Collections.Generic;
using System.Linq;
using NewWave.FastTrader.Shared.DTO.Execution;

namespace NewWave.FastTrader.Server.Blotter
{
    public class TradeRepository : ITradeRepository
    {
        private readonly List<TradeDto> _allTrades = new List<TradeDto>();

        public void StoreTrade(TradeDto trade)
        {
            lock (_allTrades)
            {
                _allTrades.Add(trade);
            }
        }

        public IList<TradeDto> GetAllTrades()
        {
            IList<TradeDto> trades;

            lock (_allTrades)
            {
                trades = _allTrades.ToList();
            }

            return trades;
        } 
    }
}