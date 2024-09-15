using System;
using System.Collections.Generic;
using NewWave.FastTrader.Client.Domain.Models.Execution;

namespace NewWave.FastTrader.Client.Domain.Repositories
{
    public interface ITradeRepository
    {
        IObservable<IEnumerable<ITrade>> GetTradesStream();
    }
}