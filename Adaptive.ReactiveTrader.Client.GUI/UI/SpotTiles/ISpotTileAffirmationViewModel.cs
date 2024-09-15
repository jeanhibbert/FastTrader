using System;
using System.Windows.Input;
using NewWave.FastTrader.Client.Domain.Models;
using NewWave.FastTrader.Shared.UI;

namespace NewWave.FastTrader.Client.UI.SpotTiles
{
    public interface ISpotTileAffirmationViewModel : IViewModel
    {
        string CurrencyPair { get; }
        Direction Direction { get; }
        long Notional { get; }
        decimal SpotRate { get; }
        string Rejected { get; }

        DateTime TradeDate { get; }
        long TradeId { get; }
        string TraderName { get; }
        DateTime ValueDate { get; }

        ICommand DismissCommand { get; }
        string DealtCurrency { get; }
        string OtherCurrency { get; }
    }
}
