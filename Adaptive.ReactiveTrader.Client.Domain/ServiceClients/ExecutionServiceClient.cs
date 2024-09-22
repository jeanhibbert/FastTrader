using System;
using System.Reactive.Linq;
using Microsoft.AspNet.SignalR.Client;
using NewWave.FastTrader.Client.Domain.Transport;
using NewWave.FastTrader.Shared;
using NewWave.FastTrader.Shared.DTO.Execution;

namespace NewWave.FastTrader.Client.Domain.ServiceClients
{
    internal class ExecutionServiceClient : ServiceClientBase, IExecutionServiceClient
    {
        public ExecutionServiceClient(IConnectionProvider connectionProvider) : base(connectionProvider)
        {
        }

        public IObservable<TradeDto> ExecuteRequest(TradeRequestDto tradeRequest)
        {
            return RequestUponConnection(connection => ExecuteForConnection(connection.ExecutionHubProxy, tradeRequest), TimeSpan.FromMilliseconds(500));
        }

        private static IObservable<TradeDto> ExecuteForConnection(IHubProxy executionHubProxy, TradeRequestDto tradeRequestDto)
        {
            return Observable.FromAsync(
                () => executionHubProxy.Invoke<TradeDto>(ServiceConstants.Server.Execute, tradeRequestDto));
        } 
    }
}
