﻿using System;
using NewWave.FastTrader.Client.Domain.Models.Execution;
using NewWave.FastTrader.Client.Domain.Models.ReferenceData;
using NewWave.FastTrader.Shared.UI;
using PropertyChanged;

namespace NewWave.FastTrader.Client.UI.SpotTiles
{
    [AddINotifyPropertyChangedInterface]
    public class SpotTileViewModel : ViewModelBase, ISpotTileViewModel
    {
        public ISpotTilePricingViewModel Pricing { get; private set; }
        public ISpotTileAffirmationViewModel Affirmation { get; private set; }
        public ISpotTileErrorViewModel Error { get; private set; }
        public ISpotTileConfigViewModel Config { get; private set; }
        public TileState State { get; private set; }

        public string CurrencyPair { get; private set; }

        private readonly Func<ITrade, ISpotTileViewModel, ISpotTileAffirmationViewModel> _affirmationFactory;
        private readonly Func<string, ISpotTileViewModel, ISpotTileErrorViewModel> _errorFactory;
        private readonly Func<ISpotTileConfigViewModel> _configFactory;
        private bool _disposed;

        public SpotTileViewModel(ICurrencyPair currencyPair,
            SpotTileSubscriptionMode spotTileSubscriptionMode,
            Func<ICurrencyPair, SpotTileSubscriptionMode, ISpotTileViewModel, ISpotTilePricingViewModel> pricingFactory,
            Func<ITrade, ISpotTileViewModel, ISpotTileAffirmationViewModel> affirmationFactory,
            Func<string, ISpotTileViewModel, ISpotTileErrorViewModel> errorFactory,
            Func<ISpotTileConfigViewModel> configFactory)
        {
            _affirmationFactory = affirmationFactory;
            _errorFactory = errorFactory;
            _configFactory = configFactory;

            if (currencyPair != null)
            {
                Pricing = pricingFactory(currencyPair, spotTileSubscriptionMode, this);
                CurrencyPair = currencyPair.Symbol;
            }
        }

        public void Dispose()
        {
            if (!_disposed)
            {
                Pricing.Dispose();
                _disposed = true;
            }
        }

        public void OnTrade(ITrade trade)
        {
            Affirmation = _affirmationFactory(trade, this);
            State = TileState.Affirmation;
            Error = null;
        }

        public void OnExecutionError(string message)
        {
            Error = _errorFactory(message, this);
            State = TileState.Error;
        }

        public void DismissAffirmation()
        {
            State = TileState.Pricing;
            Affirmation = null;
        }

        public void DismissError()
        {
            State = TileState.Pricing;
            Error = null;
        }

        public void ToConfig()
        {
            State = TileState.Config;
            Config = _configFactory();
        }
    }

    public enum TileState
    {
        Pricing,
        Affirmation,
        Error,
        Config
    }
}
