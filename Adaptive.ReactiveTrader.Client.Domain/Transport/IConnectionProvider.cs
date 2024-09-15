using System;

namespace NewWave.FastTrader.Client.Domain.Transport
{
    internal interface IConnectionProvider
    {
        IObservable<IConnection> GetActiveConnection();
    }
}