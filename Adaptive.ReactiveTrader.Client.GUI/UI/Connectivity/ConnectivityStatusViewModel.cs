﻿using System;
using System.Reactive.Linq;
using NewWave.FastTrader.Client.Concurrency;
using NewWave.FastTrader.Client.Domain;
using NewWave.FastTrader.Client.Domain.Instrumentation;
using NewWave.FastTrader.Client.Domain.Transport;
using NewWave.FastTrader.Shared.UI;
using log4net;

namespace NewWave.FastTrader.Client.UI.Connectivity
{
    class ConnectivityStatusViewModel : ViewModelBase, IConnectivityStatusViewModel
    {
        private static readonly TimeSpan StatsFrequency = TimeSpan.FromSeconds(1);

        private readonly IPriceLatencyRecorder _priceLatencyRecorder;
        private static readonly ILog Log = LogManager.GetLogger(typeof(ConnectivityStatusViewModel));

        public ConnectivityStatusViewModel(IReactiveTrader reactiveTrader, IConcurrencyService concurrencyService)
        {
            _priceLatencyRecorder = reactiveTrader.PriceLatencyRecorder;
            reactiveTrader.ConnectionStatusStream
                .ObserveOn(concurrencyService.Dispatcher)
                .SubscribeOn(concurrencyService.ThreadPool)
                .Subscribe(
                OnStatusChange,
                ex => Log.Error("An error occured within the connection status stream.", ex));

            Observable
                .Interval(StatsFrequency, concurrencyService.Dispatcher)
                .Subscribe(OnTimerTick);
        }

        private void OnTimerTick(long _)
        {
            var stats = _priceLatencyRecorder.CalculateAndReset();

            if (stats == null)
                return;

            UiLatency = stats.UiLatencyMax;
            ServerClientLatency = stats.ServerLatencyMax;
            TotalLatency = stats.TotalLatencyMax;
            UiUpdates = stats.RenderedCount;
            TicksReceived = stats.ReceivedCount;
            CpuTime = Math.Round(stats.ProcessTime.TotalMilliseconds, 0);
            CpuPercent = Math.Round(CpuTime/(Environment.ProcessorCount*StatsFrequency.TotalMilliseconds)*100, 0);
            Histogram = stats.Histogram;
        }

        private void OnStatusChange(ConnectionInfo connectionInfo)
        {
            Server = connectionInfo.Server;

            switch (connectionInfo.ConnectionStatus)
            {
                case ConnectionStatus.Uninitialized:
                case ConnectionStatus.Connecting:
                    Status = "Connecting...";
                    Disconnected = true;
                    break;
                case ConnectionStatus.Reconnected:
                case ConnectionStatus.Connected:
                    Status = "Connected";
                    Disconnected = false;
                    break;
                case ConnectionStatus.ConnectionSlow:
                    Status = "Slow connection detected";
                    Disconnected = false;
                    break;
                case ConnectionStatus.Reconnecting:
                    Status = "Reconnecting...";
                    Disconnected = true;
                    break;
                case ConnectionStatus.Closed:
                    Status = "Disconnected";
                    Disconnected = true;
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        public string Status { get; private set; }
        public string Server { get; private set; }
        public bool Disconnected { get; private set; }
        public long UiUpdates { get; private set; }
        public long TicksReceived { get; private set; }
        public long TotalLatency { get; set; }
        public long ServerClientLatency { get; private set; }
        public long UiLatency { get; private set; }
        public string Histogram { get; private set; }
        public double CpuTime { get; private set; }
        public double CpuPercent { get; private set; }
    }
}