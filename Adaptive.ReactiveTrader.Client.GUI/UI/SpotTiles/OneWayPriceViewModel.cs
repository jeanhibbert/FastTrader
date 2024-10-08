﻿using System;
using System.Reactive.Linq;
using System.Windows.Input;
using NewWave.FastTrader.Client.Concurrency;
using NewWave.FastTrader.Client.Domain.Models;
using NewWave.FastTrader.Client.Domain.Models.Execution;
using NewWave.FastTrader.Client.Domain.Models.Pricing;
using NewWave.FastTrader.Shared.Extensions;
using NewWave.FastTrader.Shared.UI;
using log4net;
using PropertyChanged;

namespace NewWave.FastTrader.Client.UI.SpotTiles
{
    [AddINotifyPropertyChangedInterface]
    public class OneWayPriceViewModel : ViewModelBase, IOneWayPriceViewModel
    {
        private readonly ISpotTilePricingViewModel _parent;
        private readonly IConcurrencyService _concurrencyService;
        private static readonly ILog Log = LogManager.GetLogger(typeof(OneWayPriceViewModel));

        private readonly DelegateCommand _executeCommand;
        private IExecutablePrice _executablePrice;

        public Direction Direction { get; private set; }
        public string BigFigures { get; private set; }
        public string Pips { get; private set; }
        public string TenthOfPip { get; private set; }
        public bool IsExecuting { get; private set; }
        public SpotTileExecutionMode ExecutionMode { get; set; }

        public OneWayPriceViewModel(Direction direction, ISpotTilePricingViewModel parent, IConcurrencyService concurrencyService)
        {
            _parent = parent;
            _concurrencyService = concurrencyService;
            Direction = direction;

            _executeCommand = new DelegateCommand(OnExecute, CanExecute);
        }

        #region ExecuteCommand
        public ICommand ExecuteCommand { get { return _executeCommand; } }

        private bool CanExecute()
        {
            return _executablePrice != null && !IsExecuting;
        }

        private void OnExecute()
        {
            long notional;
            if (!long.TryParse(_parent.Notional, out notional))
            {
                return;
            }
            IsExecuting = true;

            if (ExecutionMode == SpotTileExecutionMode.Async)
            {
                ExecuteAsync(notional);
            } else if (ExecutionMode == SpotTileExecutionMode.Sync)
            {
                ExecuteSync(notional);
            }
        }

        private void ExecuteAsync(long notional)
        {
            _executablePrice.ExecuteRequest(notional, _parent.DealtCurrency)
                .ObserveOn(_concurrencyService.Dispatcher)
                .SubscribeOn(_concurrencyService.ThreadPool)
                .Subscribe(OnExecutedResult, OnExecutionError);
        }

        private void ExecuteSync(long notional)
        {
            try
            {
                OnExecutedResult(_executablePrice.ExecuteRequest(notional, _parent.DealtCurrency).Wait());
            }
            catch (Exception ex)
            {
                OnExecutionError(ex);
            }

        }

        private void OnExecutionError(Exception exception)
        {
            if (exception is TimeoutException)
            {
                OnExecutionTimeout();
            }
            else
            {
                Log.Error("An error occured while processing the trade request.", exception);
                _parent.OnExecutionError("An error occured while executing the trade. Please check your blotter and if your position is unknown, contact your support representative.");
            }
            IsExecuting = false;
        }

        #endregion

        public void OnPrice(IExecutablePrice executablePrice)
        {
            _executablePrice = executablePrice;

            var formattedPrice = PriceFormatter.GetFormattedPrice(_executablePrice.Rate,
                executablePrice.Parent.CurrencyPair.RatePrecision, executablePrice.Parent.CurrencyPair.PipsPosition);

            BigFigures = formattedPrice.BigFigures;
            Pips = formattedPrice.Pips;
            TenthOfPip = formattedPrice.TenthOfPip;

            _executeCommand.RaiseCanExecuteChanged();
        }

        public void OnStalePrice()
        {
            _executablePrice = null;

            BigFigures = string.Empty;
            Pips = string.Empty;
            TenthOfPip = string.Empty;

            _executeCommand.RaiseCanExecuteChanged();
        }

        private void OnExecutedResult(IStale<ITrade> trade)
        {
            if (trade.IsStale)
            {
                OnExecutionTimeout();
            }
            else
            {
                Log.Info("Trade executed");
                _parent.OnTrade(trade.Update);
            }
            IsExecuting = false;
        }

        private void OnExecutionTimeout()
        {
            Log.Error("Trade execution request timed out.");
            _parent.OnExecutionError(
                "No response was received from the server, the execution status is unknown. Please contact your sales representative.");
        }
    }
}
