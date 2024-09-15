using System;
using NewWave.FastTrader.Shared.DTO.Execution;

namespace NewWave.FastTrader.Client.Domain.ServiceClients
{
    internal interface IExecutionServiceClient
    {
        IObservable<TradeDto> ExecuteRequest(TradeRequestDto tradeRequest);
    }
}