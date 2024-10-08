﻿using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using System.Windows.Threading;
using NewWave.FastTrader.Server.Pricing;
using NewWave.FastTrader.Server.ReferenceData;
using NewWave.FastTrader.Shared.Extensions;
using NewWave.FastTrader.Shared.UI;
using log4net;
using Microsoft.Owin.Hosting;

namespace NewWave.FastTrader.Server
{
    internal class MainViewModel : ViewModelBase, IMainViewModel
    {
        private const string Address = "http://localhost:8080";
        private static readonly ILog Log = LogManager.GetLogger(typeof (MainWindow));

        private readonly IPriceFeed _priceFeed;
        private readonly ICurrencyPairRepository _currencyPairRepository;
        private readonly Func<CurrencyPairInfo, ICurrencyPairViewModel> _ccyViewModelFactory;
        private readonly IPricePublisher _pricePublisher;
        private long _lastTickTotalUpdates;

        private IDisposable _signalr;
        private DispatcherTimer _timer;
        private int _updateFrequency = 100;

        public ICommand StartStopCommand { get; private set; }
        public string ServerStatus { get; private set; }
        public string StartStopCommandText { get; private set; }
        public string Throughput { get; private set; }
        public string DesiredThroughput { get; set; }
        private bool _updatingThroughput;

        public ObservableCollection<ICurrencyPairViewModel> CurrencyPairs { get; private set; } 

        public MainViewModel(
            IPricePublisher pricePublisher, 
            IPriceFeed priceFeed, 
            ICurrencyPairRepository currencyPairRepository, 
            Func<CurrencyPairInfo, ICurrencyPairViewModel> ccyViewModelFactory)
        {
            _pricePublisher = pricePublisher;
            _priceFeed = priceFeed;
            _currencyPairRepository = currencyPairRepository;
            _ccyViewModelFactory = ccyViewModelFactory;

            StartStopCommand = new DelegateCommand(StartStopServer);
            CurrencyPairs = new ObservableCollection<ICurrencyPairViewModel>();

            ObserveThroughputs();
        }

        public void Start()
        {
            StartServer();

            foreach (var cpi in _currencyPairRepository.GetAllCurrencyPairInfos())
            {
                CurrencyPairs.Add(_ccyViewModelFactory(cpi));   
            }
            UpdateFrequency = 100;
        }

        public int UpdateFrequency
        {
            get { return _updateFrequency; }
            set
            {
                _updateFrequency = value;
                _priceFeed.SetUpdateFrequency(value);
            }
        }

        private void StartStopServer()
        {
            if (_signalr != null)
            {
                StopServer();
            }
            else
            {
                StartServer();
            }
        }

        private void StopServer()
        {
            _signalr.Dispose();
            _signalr = null;
            ServerStatus = "Stopped";
            StartStopCommandText = "Start";

            if (_timer != null)
            {
                _timer.Stop();
                _timer = null;
                Throughput = "0";
            }
        }

        private void StartServer()
        {
            ServerStatus = "Starting...";

            //var options = new FileServerOptions
            //{
            //    EnableDirectoryBrowsing = true,
            //    FileSystem = new PhysicalFileSystem(@".\StaticFiles"),
            //    RequestPath = new PathString("/ReactiveTrader")
            //};

            try
            {
                _signalr = WebApp.Start(Address);
            }
            catch (Exception exception)
            {
                Log.Error("An error occured while starting SignalR", exception);
            }
            ServerStatus = "Started on " + Address;
            StartStopCommandText = "Stop";
            StartMeasuringThroughput();
        }

        private void StartMeasuringThroughput()
        {
            _lastTickTotalUpdates = _pricePublisher.TotalPricesPublished;

            _timer = new DispatcherTimer(TimeSpan.FromSeconds(1), DispatcherPriority.Normal, (sender, args) =>
            {
                long newTotalUpdates = _pricePublisher.TotalPricesPublished;
                long publishedLastSecond = newTotalUpdates - _lastTickTotalUpdates;
                _lastTickTotalUpdates = newTotalUpdates;

                Throughput = publishedLastSecond.ToString("N0");
            }, Dispatcher.CurrentDispatcher);
            _timer.Start();
        }

        private void ObserveThroughputs()
        {
            this.ObserveProperty(p => p.DesiredThroughput)
                .Subscribe(desiredThroughput =>
                {
                    if (_updatingThroughput)
                        return;
                    _updatingThroughput = true;
                    int value;
                    if (int.TryParse(desiredThroughput, out value))
                    {
                        UpdateFrequency = value;
                    }
                    _updatingThroughput = false;
                });

            this.ObserveProperty(p => p.UpdateFrequency)
                .Subscribe(updateFrequency =>
                {
                    if (_updatingThroughput)
                        return;

                    _updatingThroughput = true;
                    DesiredThroughput = updateFrequency.ToString("N0");
                    _updatingThroughput = false;
                });
        }

    }
}