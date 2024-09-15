using System;
using System.Collections.Generic;
using NewWave.FastTrader.Shared.DTO.Execution;

namespace NewWave.FastTrader.Client.Domain.ServiceClients
{
    internal interface IBlotterServiceClient
    {
        IObservable<IEnumerable<TradeDto>> GetTradesStream();
    }
}