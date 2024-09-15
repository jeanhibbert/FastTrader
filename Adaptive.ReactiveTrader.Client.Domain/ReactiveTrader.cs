using System;
using System.Reactive.Linq;
using NewWave.FastTrader.Client.Domain.Concurrency;
using NewWave.FastTrader.Client.Domain.Instrumentation;
using NewWave.FastTrader.Client.Domain.Models.Execution;
using NewWave.FastTrader.Client.Domain.Models.Pricing;
using NewWave.FastTrader.Client.Domain.Models.ReferenceData;
using NewWave.FastTrader.Client.Domain.Repositories;
using NewWave.FastTrader.Client.Domain.ServiceClients;
using NewWave.FastTrader.Client.Domain.Transport;
using log4net;

namespace NewWave.FastTrader.Client.Domain
{
    public class ReactiveTrader : IReactiveTrader, IDisposable
    {
        private ConnectionProvider _connectionProvider;
        private static readonly ILog Log = LogManager.GetLogger(typeof(ReactiveTrader));

        public void Initialize(string username, string[] servers)
        {
            _connectionProvider = new ConnectionProvider(username, servers);

            var referenceDataServiceClient = new ReferenceDataServiceClient(_connectionProvider);
            var executionServiceClient = new ExecutionServiceClient(_connectionProvider);
            var blotterServiceClient = new BlotterServiceClient(_connectionProvider);
            var pricingServiceClient = new PricingServiceClient(_connectionProvider);

            PriceLatencyRecorder = new PriceLatencyRecorder();
            var concurrencyService = new ConcurrencyService();

            var tradeFactory = new TradeFactory();
            var executionRepository = new ExecutionRepository(executionServiceClient, tradeFactory, concurrencyService);
            var priceFactory = new PriceFactory(executionRepository, PriceLatencyRecorder);
            var priceRepository = new PriceRepository(pricingServiceClient, priceFactory);
            var currencyPairUpdateFactory = new CurrencyPairUpdateFactory(priceRepository);
            TradeRepository = new TradeRepository(blotterServiceClient, tradeFactory);
            ReferenceData = new ReferenceDataRepository(referenceDataServiceClient, currencyPairUpdateFactory);
        }

        public IReferenceDataRepository ReferenceData { get; private set; }
        public ITradeRepository TradeRepository { get; private set; }
        public IPriceLatencyRecorder PriceLatencyRecorder { get; private set; }

        public IObservable<ConnectionInfo> ConnectionStatusStream
        {
            get
            {
                return _connectionProvider.GetActiveConnection()
                    .Do(_ => Log.Info("New connection created by connection provider"))
                    .Select(c => c.StatusStream)
                    .Switch()
                    .Publish()
                    .RefCount();
            }
        }

        public void Dispose()
        {
            _connectionProvider.Dispose();
        }
    }
}
