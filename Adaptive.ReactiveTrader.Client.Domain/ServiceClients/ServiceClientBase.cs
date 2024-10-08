﻿ using System;
using System.Reactive;
using System.Reactive.Linq;
using NewWave.FastTrader.Client.Domain.Transport;
using NewWave.FastTrader.Shared.Extensions;
using log4net;

namespace NewWave.FastTrader.Client.Domain.ServiceClients
{
    internal class ServiceClientBase
    {
        private readonly IConnectionProvider _connectionProvider;
        private static readonly ILog Log = LogManager.GetLogger(typeof(ServiceClientBase));
        
        protected ServiceClientBase(IConnectionProvider connectionProvider)
        {
            _connectionProvider = connectionProvider;
        }

        protected IObservable<T> GetResilientStream<T>(Func<IConnection, IObservable<T>> streamFactory, TimeSpan connectionTimeout)
        {
            var activeConnections = (from connection in _connectionProvider.GetActiveConnection()
                                     from status in connection.StatusStream
                                     where status.ConnectionStatus == ConnectionStatus.Connected || status.ConnectionStatus == ConnectionStatus.Reconnected
                                     select connection)
                .Publish()
                .RefCount();

            // get the first connection
            var firstConnection = activeConnections.Take(1).Timeout(connectionTimeout);

            // 1 - notifies when the first connection gets disconnected
            var firstDisconnection = from connection in firstConnection
                                     from status in connection.StatusStream
                                     where status.ConnectionStatus == ConnectionStatus.Reconnecting || status.ConnectionStatus == ConnectionStatus.Closed
                                     select Unit.Default;

            // 2- connection provider created a new connection it means the active one has droped
            var subsequentConnection = activeConnections.Skip(1).Select(_ => Unit.Default).Take(1);

            // OnError when we get 1 or 2
            var disconnected = firstDisconnection.Merge(subsequentConnection)
                .Select(_ => Notification.CreateOnError<T>(new TransportDisconnectedException("Connection was closed.")))
                .Dematerialize();

            // create a stream which will OnError as soon as the connection drops
            return (from connection in firstConnection
                    from t in streamFactory(connection)
                    select t)
                .Merge(disconnected)
                .Publish()
                .RefCount();
        }

        protected IObservable<T> RequestUponConnection<T>(Func<IConnection, IObservable<T>> factory, TimeSpan connectionTimeout)
        {
            return (from connection in _connectionProvider.GetActiveConnection().Take(1).Timeout(connectionTimeout)
                   from t in factory(connection)
                   select t)
                   .CacheFirstResult();
        }
    }
}
