using System;
using NewWave.FastTrader.Client.Domain.Instrumentation;
using NewWave.FastTrader.Client.Domain.Repositories;

namespace NewWave.FastTrader.Client.Domain
{
    public interface IReactiveTrader
    {
        IReferenceDataRepository ReferenceData { get; }
        ITradeRepository TradeRepository { get; }
        IObservable<ConnectionInfo> ConnectionStatusStream { get; }
        IPriceLatencyRecorder PriceLatencyRecorder { get; }
        void Initialize(string username, string[] servers);
    }
}