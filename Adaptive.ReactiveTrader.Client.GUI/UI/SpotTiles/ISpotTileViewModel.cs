using System;
using NewWave.FastTrader.Client.Domain.Models.Execution;
using NewWave.FastTrader.Shared.UI;

namespace NewWave.FastTrader.Client.UI.SpotTiles
{
    public interface ISpotTileViewModel : IViewModel, IDisposable
    {
        ISpotTilePricingViewModel Pricing { get; }
        ISpotTileAffirmationViewModel Affirmation { get; }
        ISpotTileErrorViewModel Error { get; }
        ISpotTileConfigViewModel Config { get; }
        TileState State { get; }
        string CurrencyPair { get; }
        void OnTrade(ITrade trade);
        void OnExecutionError(string message);
        void ToConfig();
        void DismissAffirmation();
        void DismissError();
    }
}
