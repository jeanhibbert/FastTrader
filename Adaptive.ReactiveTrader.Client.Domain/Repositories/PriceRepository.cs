﻿using System;
using System.Reactive.Concurrency;
using System.Reactive.Linq;
using NewWave.FastTrader.Client.Domain.Models.Pricing;
using NewWave.FastTrader.Client.Domain.Models.ReferenceData;
using NewWave.FastTrader.Client.Domain.ServiceClients;
using NewWave.FastTrader.Shared.Extensions;

namespace NewWave.FastTrader.Client.Domain.Repositories
{
    class PriceRepository : IPriceRepository
    {
        private readonly IPricingServiceClient _pricingServiceClient;
        private readonly IPriceFactory _priceFactory;

        public PriceRepository(IPricingServiceClient pricingServiceClient, IPriceFactory priceFactory)
        {
            _pricingServiceClient = pricingServiceClient;
            _priceFactory = priceFactory;
        }

        public IObservable<IPrice> GetPriceStream(ICurrencyPair currencyPair)
        {
            return Observable.Defer(() => _pricingServiceClient.GetSpotStream(currencyPair.Symbol))
                .Select(p => _priceFactory.Create(p, currencyPair))
                .Catch(Observable.Return(new StalePrice(currencyPair))) // if the stream errors (server disconnected), we push a stale price 
                .Repeat()                                               // and resubscribe
                .DetectStale(TimeSpan.FromSeconds(4),  Scheduler.Default)
                .Select(s => s.IsStale ? new StalePrice(currencyPair) : s.Update)
                .Publish()
                .RefCount();
        }
    }
}