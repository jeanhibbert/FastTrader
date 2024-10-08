﻿using NewWave.FastTrader.Client.Domain.Models;
using NewWave.FastTrader.Client.Domain.Models.Execution;
using NewWave.FastTrader.Shared.UI;

namespace NewWave.FastTrader.Client.UI.SpotTiles.Designer
{
    public class DesignTimeSpotTileViewModel : ViewModelBase, ISpotTileViewModel
    {
        public DesignTimeSpotTileViewModel()
        {
            Pricing = new DesignTimeSpotTilePricingViewModel();
            CurrencyPair = "EURUSD";
            Affirmation = null;
            Error = null;
        }

        public void Dispose()
        {
        }

        public ISpotTilePricingViewModel Pricing { get; private set; }
        public ISpotTileAffirmationViewModel Affirmation { get; private set; }
        public ISpotTileErrorViewModel Error { get; private set; }
        public ISpotTileConfigViewModel Config { get; private set; }
        public TileState State { get { return TileState.Pricing;} }
        public string CurrencyPair { get; private set; }

        public void OnTrade(ITrade trade)
        {
        }

        public void OnExecutionError(string message)
        {
        }

        public void ToConfig()
        {
        }

        public void DismissAffirmation()
        {
        }

        public void DismissError()
        {
        }
    }
}