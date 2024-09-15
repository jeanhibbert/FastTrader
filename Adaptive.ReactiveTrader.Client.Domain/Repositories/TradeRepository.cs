using System;
using System.Collections.Generic;
using System.Linq;
using System.Reactive.Linq;
using NewWave.FastTrader.Client.Domain.Models.Execution;
using NewWave.FastTrader.Client.Domain.ServiceClients;

namespace NewWave.FastTrader.Client.Domain.Repositories
{
    class TradeRepository : ITradeRepository
    {
        private readonly IBlotterServiceClient _blotterServiceClient;
        private readonly ITradeFactory _tradeFactory;

        public TradeRepository(IBlotterServiceClient blotterServiceClient, ITradeFactory tradeFactory)
        {
            _blotterServiceClient = blotterServiceClient;
            _tradeFactory = tradeFactory;
        }

        public IObservable<IEnumerable<ITrade>> GetTradesStream()
        {
            return Observable.Defer(() => _blotterServiceClient.GetTradesStream())
                .Select(trades => trades.Select(_tradeFactory.Create))
                .Catch(Observable.Return(new ITrade[0]))
                .Repeat()
                .Publish()
                .RefCount();
        }
    }
}