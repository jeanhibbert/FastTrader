using System;
using NewWave.FastTrader.Client.Domain.Models;
using NewWave.FastTrader.Shared.UI;

namespace NewWave.FastTrader.Client.UI.Blotter
{
    public interface ITradeViewModel : IViewModel
    {
        decimal SpotRate { get; }
        string Notional { get; }
        Direction Direction { get; }
        string CurrencyPair { get; }
        string TradeId { get; }
        DateTime TradeDate { get; }
        string TradeStatus { get; }
        string TraderName { get; }
        DateTime ValueDate { get; }
        bool IsNewTrade { get; }
    }
}
